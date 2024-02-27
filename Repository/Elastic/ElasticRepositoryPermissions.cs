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

            _connectionSettings = new ConnectionSettings(new SingleNodeConnectionPool(new Uri("https://localhost:9200"))).DefaultIndex("permissionsindex").
                DefaultMappingFor<ViewModelElasticPermissionsUser>(m=> m.IdProperty("PermissionGuid")
                ).DisableDirectStreaming();

            _connectionSettings.CertificateFingerprint(configuration_fingerprint.Value).BasicAuthentication("elastic", configuration_password.Value);
            _elasticsearchSettings = new Transport<ConnectionSettings>(_connectionSettings);
            _elasticsearchClient = new ElasticClient(_elasticsearchSettings);
        }
        public override List<ViewModelElasticPermissionsUser> GetPermissionsOrderedByDateUpdated(string sortField, string dateInitial)
        {
            DateTime ancla;
            List<ViewModelElasticPermissionsUser> entities = null;
            if (DateTime.TryParse(dateInitial, out ancla))
            {
                var Query = new DateRangeQuery
                { 
                    Field = sortField,
                    GreaterThanOrEqualTo = DateMath.Anchored(ancla).RoundTo(DateMathTimeUnit.Minute),
                    //Format = 
                    TimeZone = "+01:00"
                };
                var request = new SearchRequest<ViewModelElasticPermissionsUser>()
                {
                    
                    Query = Query,
                    Sort = new List<ISort>
                    {
                        new FieldSort { Field = sortField, Order = SortOrder.Descending }
                    }
                };
                var searchResponse = _elasticsearchClient.Search<ViewModelElasticPermissionsUser>(request);
                entities = new List<ViewModelElasticPermissionsUser>();
                while (searchResponse.Documents.Any())
                {
                    foreach (var document in searchResponse.Documents)
                    {
                        var entidad = new ViewModelElasticPermissionsUser { LastUpdated = document.LastUpdated, UserName = document.UserName, PermissionGuid = document.PermissionGuid, PermissionName = document.PermissionName, UserId = document.UserId };
                        entities.Add(entidad);
                    }
                    searchResponse = _elasticsearchClient.Scroll<ViewModelElasticPermissionsUser>("10s", searchResponse.ScrollId);
                }
            }
            return entities != null && entities.Count > 0 ? entities : null;
        }
    }
}
