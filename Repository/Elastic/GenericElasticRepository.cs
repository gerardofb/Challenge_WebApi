using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
namespace Repository.Elastic
{
    public class GenericElasticRepository<TEntity> where TEntity : class
    {
        internal ElasticsearchClient _elasticsearchClient;
        internal ElasticsearchClientSettings _elasticsearchSettings;
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
        public virtual void InsertPriorPermissions()
        {
            
        }
        public virtual void InsertAfterPermissions()
        {

        }
        public virtual List<TEntity> GetPermissionsOrderedByDateUpdated()
        {
            throw new NotImplementedException();
        }
    }
}
