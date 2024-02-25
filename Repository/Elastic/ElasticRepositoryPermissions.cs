using Microsoft.Extensions.Configuration;
using Infrastructure.ElasticViewModels;

namespace Repository.Elastic
{
    public class ElasticRepositoryPermissions<TEntity> : GenericElasticRepository<ViewModelElasticPermissionsUser>
    {
        public ElasticRepositoryPermissions(IConfigurationRoot configuration) : base(configuration)
        {

        }
    }
}
