using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Elastic.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Models;

namespace Cached.Elastic.Repository
{
    public class CachedElasticRepository
    {
        private readonly ElasticRepository _elasticRepository;
        private readonly IDistributedCache _distributedCache;

        public CachedElasticRepository(ElasticRepository elasticRepository, IDistributedCache distributedCache)
        {
            _elasticRepository = elasticRepository;   
            _distributedCache = distributedCache;
        }

        public IEnumerable<ResultDocument> Search(String search)
        {
            var cachedDocuments = _distributedCache.Get(search);
            if(cachedDocuments == null) {
                var documents = _elasticRepository.Search(search);
                var bf = new BinaryFormatter();

                using (var ms = new MemoryStream())
                {
                    bf.Serialize(ms, documents);
                    _distributedCache.Set(search, ms.ToArray(), new DistributedCacheEntryOptions{
                        AbsoluteExpirationRelativeToNow = new TimeSpan(0, 2, 0)
                    });
                }

                return documents;
            }

            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(cachedDocuments, 0, cachedDocuments.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = (IEnumerable<ResultDocument>)binForm.Deserialize(memStream);
                return obj;
            }
        }
    }
}
