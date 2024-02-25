using NUnit.Framework;
using Repository.Implementation;
using Infrastructure.Contexts;
using Infrastructure.Models;
using Repository.UnitOfWork;

namespace TestChallengeWebApi.TestsRepository
{
    public class TestRepositoryPermissionType
    {
        private ChallengeContext context;
        private RepositoryPermissionType<PermissionType> repository;
        private UnitOfWorkPermissions unitOfWork; 
        [SetUp]
        public void Setup()
        {
            context = new FactoryDbContext.ChallengeContextFactory().CreateDbContext(new string[] { });
            unitOfWork = new UnitOfWorkPermissions(context);
            repository = (RepositoryPermissionType<PermissionType>)unitOfWork.PermissionTypeRepository;
        }

        [Test]
        public void TestInsertPermissionType()
        {
            PermissionType permissionType = new PermissionType() { Name = "Superusuario" };
            repository.Insert(permissionType);
            Assert.IsTrue(context.PermissionsTypes.Local.Contains(permissionType));
        }
    }
}
