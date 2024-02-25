using NUnit.Framework;
using Repository.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Models;
using System.Collections.Generic;

namespace TestChallengeWebApi.TestsRepository
{
    public class TestRepositoryPermissionType
    {
        private ChallengeContext context;
        private IPermissionTypeRepository repository;
        [SetUp]
        public void Setup()
        {
            context = new FactoryDbContext.ChallengeContextFactory().CreateDbContext(new string[] { });
            repository = new Repository.Implementation.RepositoryPermissionType(context);
        }

        [Test]
        public void TestInsertPermissionType()
        {
            PermissionType permissionType = new PermissionType() { Name = "Superusuario" };
            repository.InsertPermissionType<PermissionType>(permissionType);
            Assert.IsTrue(context.PermissionsTypes.Local.Contains(permissionType));
        }
    }
}
