using Elasticsearch.Net;
using Infrastructure.ElasticViewModels;
using Microsoft.Extensions.Configuration;
using Nest;

namespace Repository.Elastic
{
    public abstract class GenericElasticRepository<TEntity> where TEntity : class
    {
        internal IElasticClient _elasticsearchClient;
        internal Transport<ConnectionSettings> _elasticsearchSettings;
        internal TEntity _entity;
        internal ConnectionSettings _connectionSettings;
        public IElasticClient ElasticsearchClient
        {
            get { return _elasticsearchClient; }
        }
        public GenericElasticRepository(IConfigurationRoot _configuration)
        {
            
        }
        public virtual async Task<TEntity> InsertPriorPermissions(TEntity priorPermission)
        {
            IndexResponse? indexResponse = await _elasticsearchClient.IndexAsync<TEntity>(new IndexRequest<TEntity>(priorPermission));
            if (!indexResponse.IsValid)
            {
                return null;
            }
            else
            {
                _entity = priorPermission;
            }
            return _entity;
        }

        public virtual async Task<List<TEntity>> InsertBulkPriorPermissions(List<TEntity> priorPermissions, CancellationToken token)
        {
            if (Task.Run(() =>
             {
                 var indexBulkResponse = _elasticsearchClient.BulkAll<TEntity>(priorPermissions, b => b.
                 BackOffTime("30s")
                 .BackOffRetries(4)
                 .RefreshOnCompleted()
                 .MaxDegreeOfParallelism(Environment.ProcessorCount)
                 .Size(30));
             }).Wait(TimeSpan.FromMinutes(5)))
            {
                return await Task.Run(()=>priorPermissions);
            }
            return null;
        }
        
    }
}
