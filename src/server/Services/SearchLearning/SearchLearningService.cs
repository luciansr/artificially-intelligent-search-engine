using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services.SearchLearning
{
    public class SearchLearningService
    {
        private const int NUMBER_OF_CLICKS_TO_RECALCULATE = 10;
        private JavascriptExecutor _javascriptExecutor;

        //move to redis when finished
        public static Dictionary<String, Dictionary<int, int>> itemsClickedInSearch = new Dictionary<string, Dictionary<int, int>>();
        //move to redis when finished
        public static Dictionary<String, String> trainedModels;

        public SearchLearningService(
            JavascriptExecutor javascriptExecutor)
        {
            _javascriptExecutor = javascriptExecutor;
        }

        public IEnumerable<NeuralItemResult> OrderOffers(IEnumerable<Offer> offers, string query)
        {
            Dictionary<int, int> itemsClickedInThisSearch = null;

            if (itemsClickedInSearch.ContainsKey(query))
            {
                itemsClickedInThisSearch = itemsClickedInSearch[query];
            }

            return offers.Select(offer =>
            {
                NeuralItemResult neuralItem = new NeuralItemResult();
                neuralItem.Item = offer;
                neuralItem.NeuralOrder = 1;
                if (itemsClickedInThisSearch != null && itemsClickedInThisSearch.ContainsKey(offer.id))
                {
                    neuralItem.LeadInQuery = itemsClickedInThisSearch[offer.id];
                }
                return neuralItem;
            });
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
            if (!itemsClickedInSearch.ContainsKey(query)) return;

            // var itemsOfSearch = _elasticService.Search(query);

            List<OfferItemClicked> itemsClicked = new List<OfferItemClicked>();

            // foreach (var item in itemsOfSearch)
            // {
            //     OfferItemClicked itemClicked = new OfferItemClicked();
            //     itemClicked.Item = item.Item;
            //     // itemsClickedInSearch
            // }

            // itemsClickedInSearch[query]
        }
    }
}