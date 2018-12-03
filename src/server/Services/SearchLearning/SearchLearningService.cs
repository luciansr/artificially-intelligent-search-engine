using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Repository;
using Models;

namespace Services.SearchLearning
{
    public class SearchLearningService
    {
        private const int NUMBER_OF_CLICKS_TO_RECALCULATE = 10;
        private JavascriptExecutor _javascriptExecutor;
        private ElasticRepository _elasticRepository;

        //move to redis when finished
        public static Dictionary<String, Dictionary<int, int>> itemsClickedInSearch = new Dictionary<string, Dictionary<int, int>>();
        //move to redis when finished
        public static Dictionary<String, String> trainedModels;

        public SearchLearningService(
            JavascriptExecutor javascriptExecutor,
            ElasticRepository elasticRepository)
        {
            _javascriptExecutor = javascriptExecutor;
            _elasticRepository = elasticRepository;
        }

        public IEnumerable<NeuralItemResult> OrderOffers(IEnumerable<Offer> offers, string query)
        {
            var neuralItems = GetLeadData(offers, query);
            if (neuralItems == null) return null;

            neuralItems = OrderByNeuralNetwork(neuralItems, query);

            return neuralItems;
        }

        private List<NeuralItemResult> GetLeadData(IEnumerable<Offer> offers, string query)
        {
            if (offers == null) return null;

            Dictionary<int, int> itemsClickedInThisSearch = null;

            if (!String.IsNullOrEmpty(query) && itemsClickedInSearch.ContainsKey(query))
            {
                itemsClickedInThisSearch = itemsClickedInSearch[query];
            }

            var neuralItems = offers.Select(offer =>
            {
                NeuralItemResult neuralItem = new NeuralItemResult();
                neuralItem.Item = offer;
                neuralItem.NeuralOrder = 1;
                if (itemsClickedInThisSearch != null && itemsClickedInThisSearch.ContainsKey(offer.id))
                {
                    neuralItem.LeadInQuery = itemsClickedInThisSearch[offer.id];
                }
                return neuralItem;
            }).ToList();

            return neuralItems;
        }

        private List<NeuralItemResult> OrderByNeuralNetwork(List<NeuralItemResult> neuralItems, string query)
        {
            var neuralData = GetNeuralTestData(neuralItems);
            var order = ExecuteOrdering(neuralData, query);

            for (int i = 0; i < order.Count(); ++i)
            {
                neuralItems[i].NeuralOrder = order[i];
            }

            return neuralItems;
        }

        private List<int> ExecuteOrdering(NeuralTestData neuralTestData, string query)
        {
            //TODO execute javascript to predict values
            return neuralTestData.xs.Select(x => 1).ToList();
        }

        private NeuralTestData GetNeuralTestData(IEnumerable<NeuralItemResult> items)
        {
            NeuralTestData data = new NeuralTestData();

            data.xs = items.Select(i => new List<int> {
                i.Item.height,
                i.Item.width,
                i.Item.weight
            });

            return data;
        }

        private NeuralTrainingData GetNeuralTrainingData(IEnumerable<NeuralItemResult> items)
        {
            var itemsToUse = items.Where(i => i.LeadInQuery > 0);
            NeuralTrainingData data = new NeuralTrainingData();

            data.xs = itemsToUse.Select(i => new List<int> {
                i.Item.height,
                i.Item.width,
                i.Item.weight
            });

            data.ys = itemsToUse.Select(i => i.LeadInQuery);

            return data;
        }

        public void ItemClicked(string query, int id)
        {
            Dictionary<int, int> queryItemClickedCount = null;
            if (!itemsClickedInSearch.ContainsKey(query))
            {
                queryItemClickedCount = new Dictionary<int, int>();
                itemsClickedInSearch.Add(query, queryItemClickedCount);
            }
            else
            {
                queryItemClickedCount = itemsClickedInSearch[query];
            }

            if (!queryItemClickedCount.ContainsKey(id))
            {
                queryItemClickedCount.Add(id, 1);
            }
            else
            {
                queryItemClickedCount[id] += 1;
            }

            if (queryItemClickedCount.Values.Sum() > NUMBER_OF_CLICKS_TO_RECALCULATE)
            {
                RecalculateNeuralNetwork(query);
            }
        }

        private void RecalculateNeuralNetwork(string query)
        {
            if (itemsClickedInSearch == null || !itemsClickedInSearch.ContainsKey(query)) return;
            var offers = _elasticRepository.SearchOffer(query);

            var neuralItems = GetLeadData(offers, query);
            if(neuralItems == null) return;

            var neuralTrainingData = GetNeuralTrainingData(neuralItems);

            if(neuralTrainingData == null || neuralTrainingData.xs.Count() == 0) return;

            // TODO recalculate model and save on the training static dictionary
        }
    }
}