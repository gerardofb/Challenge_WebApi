using NUnit.Framework;
using Repository.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Models;
using System.Collections.Generic;

namespace TestChallengeWebApi
{
    public class TestRepositoryEmployee
    {
        private ChallengeContext context;
        private IRepositoryEmployee repository;
        [SetUp]
        public void Setup()
        {
            context = new FactoryDbContext.ChallengeContextFactory().CreateDbContext(new string[] { });
            repository = new Repository.Implementation.RepositoryEmployee(context);
        }

        [Test]
        public void TestInsertEmployee()
        {
            Employee employee = new Employee() { Name = "Nuevo Empleado", LastName = "Apellidos", WorkArea = new List<WorkArea>() { new WorkArea { AreaName = "Ventas" } }, PermissionEmployees = new List<PermissionsEmployee>() { new PermissionsEmployee { PermissionTypes = new PermissionType { Name = "Superusuario" } } } };
            repository.InsertEmployee<Employee>(employee);
            Assert.IsTrue(context.Employees.Local.Contains(employee));
        }
    }
}