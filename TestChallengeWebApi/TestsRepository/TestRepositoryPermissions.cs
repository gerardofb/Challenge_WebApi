using NUnit.Framework;
using Repository.Implementation;
using Repository.UnitOfWork;
using Infrastructure.Contexts;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace TestChallengeWebApi.TestsRepository
{
    public class TestRepositoryPermissions
    {
        private ChallengeContext context;
        private RepositoryPermission<PermissionsEmployee> repository;
        private UnitOfWorkPermissions unitOfWork;
        [SetUp]
        public void Setup()
        {
            context = new FactoryDbContext.ChallengeContextFactory().CreateDbContext(new string[] { });
            unitOfWork = new UnitOfWorkPermissions(context);
            repository = (RepositoryPermission<PermissionsEmployee>)unitOfWork.PermissionRepository;
        }

        [Test]
        public void TestInsertPermission()
        {
            PermissionsEmployee permission = new PermissionsEmployee() { PermissionTypes = new PermissionType() { Name = "Superusuario" }, Employees = new List<Employee> { new Employee { Name = "Nuevo usuario" } } };
            repository.Insert(permission);
            Assert.IsTrue(context.Permissions.Local.Contains(permission));
        }
    }
}
