using System;
using System.Collections.Generic;
using Elastic.Repository;
using Models;

namespace Services
{
    public class ElasticService
    {
        private ElasticRepository _elasticRepository;

        public ElasticService(ElasticRepository elasticRepository)
        {
            _elasticRepository = elasticRepository;
        }
        public IEnumerable<ResultDocument> Search(String query) {
            return _elasticRepository.Search(query);
        }
    }
}
