using Microsoft.Extensions.Configuration;
using Infrastructure.ElasticViewModels;

namespace Repository.Elastic
{
    public class ElasticRepositoryPermissions<TEntity> : GenericElasticRepository<ViewModelElasticPermissionsUser>
    {
        public ElasticRepositoryPermissions(IConfigurationRoot configuration) :base(configuration)  
        {
            _connectionSettings = new Nest.ConnectionSettings().DefaultIndex("permissionsindex").
                DefaultMappingFor<ViewModelElasticPermissionsUser>(m=> { m.IndexName("permissionsindex"); m.IdProperty("PermissionGuid")
            _elasticsearchSettings = new Transport<ConnectionSettings>(new ConnectionSettings(
                new SingleNodeConnectionPool(new Uri("https://localhost:9200"))));
            _elasticsearchClient = new ElasticClient(_elasticsearchSettings);
        }
    }
}
