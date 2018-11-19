using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Models;
using Nest;

namespace Elastic.Repository
{
    public class ElasticRepository
    {
        private IElasticClient _elasticClient;

        public ElasticRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public IEnumerable<ResultDocument> Search(String search)
        {
            var result = _elasticClient.LowLevel.Search<SearchResponse<ResultDocument>>("search", "offers", PostData.Serializable(new
            {
                query = new
                {
                    match = new
                    {
                        title = new
                        {
                            query = "jogo ps4",
                            fuzziness = 2,
                            prefix_length = 1
                        }
                    }
                }
            }));

            if (result.IsValid)
            {
                return result.Documents;
            }

            return null;
        }
    }
}
