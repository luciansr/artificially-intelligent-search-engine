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
        private ElasticService _elasticService;

        //move to redis when finished
        public static Dictionary<String, Dictionary<string, int>> itemsClickedInSearch;
        //move to redis when finished
        public static Dictionary<String, String> trainedModels;

        public SearchLearningService(
            JavascriptExecutor javascriptExecutor,
            ElasticService elasticService)
        {
            _javascriptExecutor = javascriptExecutor;
            _elasticService = elasticService;
        }

        public void ItemClicked(string query, string id)
        {
            Dictionary<string, int> queryItemClickedCount = null;
            if (!itemsClickedInSearch.ContainsKey(query))
            {
                queryItemClickedCount = new Dictionary<string, int>();
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

            var itemsOfSearch = _elasticService.Search(query);

            List<OfferItemClicked> itemsClicked = new List<OfferItemClicked>();

            foreach (var item in itemsOfSearch)
            {
                OfferItemClicked itemClicked = new OfferItemClicked();
                itemClicked.Item = item;
                // itemsClickedInSearch
            }

            // itemsClickedInSearch[query]
        }
    }
}