using Elasticsearch.Net;
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
            //IConfigurationSection configuration_fingerprint = _configuration.GetSection("FingerprintElastic").GetSection("DefaultNode");
            //IConfigurationSection configuration_password = _configuration.GetSection("PasswordElastic");
            //_elasticsearchSettings = new Transport<ConnectionSettings>(new ConnectionSettings(
            //    new SingleNodeConnectionPool(new Uri("https://localhost:9200"))));
            //_elasticsearchClient = new ElasticClient(_elasticsearchSettings);
        }
        public virtual async Task<TEntity> InsertPriorPermissions(TEntity priorPermissions)
        {
            await Task.Delay(10000).ContinueWith((a) =>
            {
                _entity =  GetPermissionsOrderedByDateUpdated().FirstOrDefault();
            });
            return _entity;
        }
        public virtual async Task<TEntity> InsertAfterPermissions(TEntity latePermissions)
        {
            await Task.Delay(10000).ContinueWith((a) =>
            {
                _entity = GetPermissionsOrderedByDateUpdated().FirstOrDefault();
            });
            return _entity;
        }
        public virtual List<TEntity> GetPermissionsOrderedByDateUpdated()
        {
            return new List<TEntity> { _entity };
        }
    }
}
