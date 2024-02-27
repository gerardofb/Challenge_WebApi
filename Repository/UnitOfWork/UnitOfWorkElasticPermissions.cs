using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Infrastructure.ElasticViewModels;
using Nest;
using Elasticsearch.Net;
using Repository.Elastic;

namespace Repository.UnitOfWork
{
    public class UnitOfWorkElasticPermissions : IDisposable
    {
        private IConfigurationRoot configuration;
        private bool disposed = false;
        private ElasticRepositoryPermissions<ViewModelElasticPermissionsUser> _elasticRepositoryPermissions;
        public UnitOfWorkElasticPermissions(IConfigurationRoot _configuration)
        {
            configuration = _configuration;
        }
        public GenericElasticRepository<ViewModelElasticPermissionsUser> ElasticRepositoryPermissions
        {
            get
            {
                if (this._elasticRepositoryPermissions == null)
                {
                    this._elasticRepositoryPermissions = new ElasticRepositoryPermissions<ViewModelElasticPermissionsUser>(configuration);
                }
                return this._elasticRepositoryPermissions;
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //_elasticRepositoryPermissions.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
