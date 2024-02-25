using Repository.Interfaces;
using Infrastructure.Models;
using Infrastructure.Contexts;
using Microsoft.Extensions.Configuration.Json;
using TestChallengeWebApi.FactoryDbContext;
using Repository.Implementation;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Inicio del programa de auto alimentación de la base de datos. Correr una sola vez!");
ChallengeContext context = new ChallengeContextFactory().CreateDbContext(new string[] { });
RepositoryEmployee repositoryEmployee = new Repository.Implementation.RepositoryEmployee(context);
WorkArea workArea = new WorkArea() { AreaName = "Ventas" };
WorkArea workArea2 = new WorkArea() { AreaName = "Administración" };
WorkArea workArea3 = new WorkArea() { AreaName = "Recursos Humanos" };
WorkArea workArea4 = new WorkArea() { AreaName = "Finanzas" };

using (var transaction = context.Database.BeginTransaction())
{
    try
    {
        context.WorkAreas.AddRange(workArea, workArea2, workArea3, workArea4);
        context.SaveChanges();
        Employee employee = new Employee() { Name = "Gerardo", LastName = "Flores Barrié", WorkArea = new List<WorkArea> { workArea }, LastUpdated = DateTime.UtcNow };
        Employee employee2 = new Employee() { Name = "Ricardo", LastName = "Rodríguez López", WorkArea = new List<WorkArea> { workArea2 }, LastUpdated = DateTime.UtcNow };
        Employee employee3 = new Employee() { Name = "Eric", LastName = "Del Valle Hernández", WorkArea = new List<WorkArea> { workArea3 }, LastUpdated = DateTime.UtcNow };
        Employee employee4 = new Employee() { Name = "Violeta", LastName = "Santos Bahena", WorkArea = new List<WorkArea> { workArea4 }, LastUpdated = DateTime.UtcNow };
        Employee employee5 = new Employee() { Name = "Eduardo", LastName = "Ramírez Alvarez", WorkArea = new List<WorkArea> { workArea }, LastUpdated = DateTime.UtcNow };
        Employee employee6 = new Employee() { Name = "Juan Víctor", LastName = "Sánchez Rodríguez", WorkArea = new List<WorkArea> { workArea2 }, LastUpdated = DateTime.UtcNow };
        Employee employee7 = new Employee() { Name = "Oscar", LastName = "Juárez Flores", WorkArea = new List<WorkArea> { workArea3 }, LastUpdated = DateTime.UtcNow };
        Employee employee8 = new Employee() { Name = "Jessica", LastName = "Sánchez Fernández", WorkArea = new List<WorkArea>{workArea4}, LastUpdated = DateTime.UtcNow };
        Employee employee9 = new Employee() { Name = "Fernando", LastName = "Avalos Rodríguez", WorkArea = new List<WorkArea> { workArea }, LastUpdated = DateTime.UtcNow };
        Employee employee10 = new Employee() { Name = "Antonio", LastName = "Ramírez Rodríguez", WorkArea = new List<WorkArea>{workArea2}, LastUpdated = DateTime.UtcNow };
        repositoryEmployee.InsertEmployee<Employee>(employee);
        repositoryEmployee.InsertEmployee<Employee>(employee2);
        repositoryEmployee.InsertEmployee<Employee>(employee3);
        repositoryEmployee.InsertEmployee<Employee>(employee4);
        repositoryEmployee.InsertEmployee<Employee>(employee5);
        repositoryEmployee.InsertEmployee<Employee>(employee6);
        repositoryEmployee.InsertEmployee<Employee>(employee7);
        repositoryEmployee.InsertEmployee<Employee>(employee8);
        repositoryEmployee.InsertEmployee<Employee>(employee9);
        repositoryEmployee.InsertEmployee<Employee>(employee10);
        repositoryEmployee.Save();
        
        transaction.Commit();
    }
    catch (Exception ex)
    {
        Console.WriteLine(String.Format("Excepción encontrada {0}", ex.ToString()));
        transaction.Rollback();
    }
}
Console.WriteLine("Programa de auto alimentación de DB finalizado");
