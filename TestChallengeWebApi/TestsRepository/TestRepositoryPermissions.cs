using NUnit.Framework;
using Repository.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace TestChallengeWebApi.TestsRepository
{
    public class TestRepositoryPermissions
    {
        private ChallengeContext context;
        private IRepositoryPermissionsEmployee repository;
        [SetUp]
        public void Setup()
        {
            context = new FactoryDbContext.ChallengeContextFactory().CreateDbContext(new string[] { });
            repository = new Repository.Implementation.RepositoryPermission(context);
        }

        [Test]
        public void TestInsertPermission()
        {
            PermissionsEmployee permission = new PermissionsEmployee() { PermissionTypes = new PermissionType() { Name = "Superusuario" }, Employees = new List<Employee> { new Employee { Name = "Nuevo usuario" } } };
            repository.InsertPermission<PermissionsEmployee>(permission);
            Assert.IsTrue(context.Permissions.Local.Contains(permission));
        }
    }
}
