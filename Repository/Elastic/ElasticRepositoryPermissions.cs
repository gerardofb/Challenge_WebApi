using Microsoft.Extensions.Configuration;
using Infrastructure.ElasticViewModels;
using Nest;
using Elasticsearch.Net;

namespace Repository.Elastic
{
    public class ElasticRepositoryPermissions<TEntity> : GenericElasticRepository<ViewModelElasticPermissionsUser>
    {
        public ElasticRepositoryPermissions(IConfigurationRoot configuration) : base(configuration)
        {
            IConfigurationSection configuration_fingerprint = configuration.GetSection("FingerprintElastic").GetSection("DefaultNode");
            IConfigurationSection configuration_password = configuration.GetSection("PasswordElastic");

            _connectionSettings = new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://es01:9200"))).DefaultIndex("permissionsindex").
                DefaultMappingFor<ViewModelElasticPermissionsUser>(m=> m.IdProperty("PermissionGuid")
                ).DisableDirectStreaming();

            _connectionSettings//.CertificateFingerprint(configuration_fingerprint.Value)
                .EnableApiVersioningHeader()
                .BasicAuthentication("elastic", configuration_password.Value);
            _elasticsearchSettings = new Transport<ConnectionSettings>(_connectionSettings);
            _elasticsearchClient = new ElasticClient(_elasticsearchSettings);
        }
        
    }
}
