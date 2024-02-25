using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Implementation;
using Queries.Interfaces;
using Infrastructure.Models;
using Infrastructure.Contexts;
using Challenge_WebApi.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Challenge_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionsController : ControllerBase
    {
        private IRepositoryEmployee repositoryEmployee;
        private IRepositoryPermissionsEmployee repositoryPermissions;
        private IPermissionTypeRepository repositoryPermissionType;
        private IQueryPermissions queryPermissions;
        private ChallengeContext context;
        public UserPermissionsController(ChallengeContext contexto, IRepositoryEmployee repositoryemp,
            IRepositoryPermissionsEmployee repositoryperm,
            IPermissionTypeRepository repositorypermtype,
            IQueryPermissions queryPerms)
        {
            repositoryEmployee = repositoryemp;
            repositoryPermissions = repositoryperm;
            repositoryPermissionType = repositorypermtype;
            queryPermissions = queryPerms;
            context = contexto;
        }
        // GET: api/<ChallengeController>

        [HttpGet("GetPermissions/{id}")]
        public IEnumerable<ViewModelPermissionsUser> GetPermissions(int id)
        {
            List<ViewModelPermissionsUser> salida = null;
            List<PermissionsEmployee> permissions = null;
            try
            {
                Employee employee = queryPermissions.Get(new Employee { Id = id });
                permissions = queryPermissions.Get(id);
                salida = new List<ViewModelPermissionsUser>();
                if (permissions != null)
                {
                    foreach (PermissionsEmployee permission in permissions)
                    {
                        PermissionType namePermission = queryPermissions.Get(permission);
                        salida.Add(new ViewModelPermissionsUser
                        {
                            LastUpdated = permission.LastUpdated,
                            UserName = String.Format("{0} {1}", employee.Name, employee.LastName),
                            PermissionGuid = permission.Guid,
                            PermissionName = namePermission != null ? namePermission.Name : null,
                            UserId = employee.Id
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return salida.Count > 0 ? salida : null;
        }

        // GET api/<ChallengeController>/5
        [HttpGet("GetUser/{name:alpha}")]
        public List<ViewModelUser> GetUser(string name)
        {
            List<Employee> employees = null;
            List<ViewModelUser> users = null;
            try
            {
                employees = queryPermissions.Get(name);
                users = new List<ViewModelUser>();
                if (employees != null)
                {
                    foreach (Employee emp in employees)
                    {
                        users.Add(new ViewModelUser
                        {
                            Id = emp.Id,
                            Name = String.Format("{0} {1}", emp.Name, emp.LastName),
                            LastUpdated = emp.LastUpdated,
                            AreasLaborales = emp.WorkArea.Select(d => d.AreaName).ToList()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return users.Count > 0 ? users : null;
        }

        // POST api/<ChallengeController>
        [HttpPost("ModifyPermission/{id}")]
        public StatusCodeResult Post(int id, [FromBody] ViewModelChangePermission newPermissions)
        {
            try
            {
                Employee employee = queryPermissions.Get(new Employee { Id = id });
                List<PermissionsEmployee> permissionschange = queryPermissions.Get(id);

                if (employee != null && permissionschange.Count > 0)
                {

                    IEnumerable<string> oldPermissions = from a in permissionschange
                                                         select a.PermissionTypes != null ?
                                                         a.PermissionTypes.Name : "";
                    if (oldPermissions.Any(x=> x == newPermissions.OldPermission))
                    {

                        PermissionType modifiedPermission = permissionschange.Select(d => d.PermissionTypes)
                            .First(a => a.Name == newPermissions.OldPermission);

                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                modifiedPermission.CreatedDate = DateTime.UtcNow;
                                modifiedPermission.Name = newPermissions.NewPermission;
                                repositoryPermissionType.UpdatePermissionType<PermissionType>(modifiedPermission);
                                repositoryPermissionType.Save();
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex.Message);
                                transaction.Rollback();
                                throw ex;
                            }
                        }
                        return Ok();
                    }
                    
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<ChallengeController>/5
        [HttpPut("RequestPermission/{id}")]
        public StatusCodeResult RequestPermission(int id, [FromBody] string permissionValue)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Employee employee = queryPermissions.Get(new Employee { Id = id });
                    PermissionType permType = new PermissionType() { Name = permissionValue, CreatedDate = DateTime.UtcNow };
                    repositoryPermissionType.InsertPermissionType<PermissionType>(permType);
                    repositoryPermissionType.Save();
                    repositoryPermissions.InsertPermission<PermissionsEmployee>(new PermissionsEmployee { Employees = new List<Employee>() { employee }, Guid = Guid.NewGuid(), LastUpdated = DateTime.UtcNow, PermissionTypes = permType });
                    repositoryPermissions.Save();
                    transaction.Commit();
                    return Ok();
                }
                catch (NotImplementedException ex)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return NotFound();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return BadRequest();
                }

            }
        }

        // DELETE api/<ChallengeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
