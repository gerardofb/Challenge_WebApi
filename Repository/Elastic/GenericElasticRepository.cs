using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;


namespace Repository.Elastic
{
    public class GenericElasticRepository<TEntity> where TEntity : class
    {
        internal ElasticsearchClient _elasticsearchClient;
        internal ElasticsearchClientSettings _elasticsearchSettings;
        internal TEntity _entity;
        public ElasticsearchClient ElasticsearchClient
        {
            get { return _elasticsearchClient; }
        }
        public GenericElasticRepository(IConfigurationRoot _configuration)
        { 
            IConfigurationSection configuration_fingerprint = _configuration.GetSection("FingerprintElastic").GetSection("DefaultNode");
            IConfigurationSection configuration_password = _configuration.GetSection("PasswordElastic");
            _elasticsearchSettings = new ElasticsearchClientSettings(
                new Uri("https://localhost:9200")).CertificateFingerprint(configuration_fingerprint.Value).Authentication(new BasicAuthentication("elastic", configuration_password.Value));
            _elasticsearchClient = new ElasticsearchClient(_elasticsearchSettings);
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
