using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Repository;
using Models;
using Services.SearchLearning;

namespace Services
{
    public class ElasticService
    {
        private ElasticRepository _elasticRepository;
        private SearchLearningService _searchLearningService;

        public ElasticService(ElasticRepository elasticRepository, SearchLearningService searchLearningService)
        {
            _elasticRepository = elasticRepository;
            _searchLearningService = searchLearningService;
        }
        public IEnumerable<NeuralItemResult> Search(String query)
        {
            var offers = _elasticRepository.SearchOffer(query);
            if(offers == null) return null;
            var orderedOffers = _searchLearningService.OrderOffers(offers, query);
            return orderedOffers.Take(10);
        }
    }
}
