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
