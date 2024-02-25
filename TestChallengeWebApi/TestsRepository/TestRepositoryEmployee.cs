using NUnit.Framework;
using Repository.Implementation;
using Repository.UnitOfWork;
using Infrastructure.Contexts;
using Infrastructure.Models;
using System.Collections.Generic;

namespace TestChallengeWebApi
{
    public class TestRepositoryEmployee
    {
        private ChallengeContext context;
        private RepositoryEmployee<Employee> repository;
        private UnitOfWorkPermissions unitOfWork;
        [SetUp]
        public void Setup()
        {
            context = new FactoryDbContext.ChallengeContextFactory().CreateDbContext(new string[] { });
            unitOfWork = new UnitOfWorkPermissions(context);
            repository = (RepositoryEmployee<Employee>)unitOfWork.EmployeeRepository;
        }

        [Test]
        public void TestInsertEmployee()
        {
            Employee employee = new Employee() { Name = "Nuevo Empleado", LastName = "Apellidos", WorkArea = new List<WorkArea>() { new WorkArea { AreaName = "Ventas" } }, PermissionEmployees = new List<PermissionsEmployee>() { new PermissionsEmployee { PermissionTypes = new PermissionType { Name = "Superusuario" } } } };
            repository.Insert(employee);
            Assert.IsTrue(context.Employees.Local.Contains(employee));
        }
    }
}