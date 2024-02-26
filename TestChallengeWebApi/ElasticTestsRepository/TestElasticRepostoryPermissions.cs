using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Repository.Elastic;
using Nest;
using Infrastructure.ElasticViewModels;
using Microsoft.Extensions.Configuration;

namespace TestChallengeWebApi.ElasticTestsRepository
{
    internal class TestElasticRepostoryPermissions
    {
        private ElasticRepositoryPermissions<ViewModelElasticPermissionsUser> repositoryElasticPermissions;
        [SetUp]
        public void Setup()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json").Build();
            repositoryElasticPermissions = new ElasticRepositoryPermissions<ViewModelElasticPermissionsUser>(configuration);
        }

        [Test]
        public void TestInsertEmployee()
        {
            var response = repositoryElasticPermissions.ElasticsearchClient?.GetAsync<ViewModelElasticPermissionsUser>(1, idx => idx.Index("my-tweet-index")).Result;
            Assert.IsFalse(response.IsValid);
        }
    }
}
