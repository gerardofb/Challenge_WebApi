using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.UnitOfWork;
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
        private UnitOfWorkPermissions unitOfWork;
        private IQueryPermissions queryPermissions;
        private ChallengeContext context;
        public UserPermissionsController(ChallengeContext contexto,
            IQueryPermissions queryPerms)
        {
            unitOfWork = new UnitOfWorkPermissions(contexto);
            queryPermissions = queryPerms;
            context = contexto;
        }
        // GET: api/<ChallengeController>

        [HttpGet("GetPermissions/{id}")]
        public IEnumerable<ViewModelPermissionsUser> GetPermissions(int id)
        {
            List<ViewModelPermissionsUser> salida = null;
            List<MaterializedViewPermissions> permissions = null;
            try
            {
                Employee employee = queryPermissions.Get(new Employee { Id = id });
                permissions = queryPermissions.Get(id);
                salida = new List<ViewModelPermissionsUser>();
                if (permissions != null)
                {
                    foreach (MaterializedViewPermissions permission in permissions)
                    {
                       
                        salida.Add(new ViewModelPermissionsUser
                        {
                            LastUpdated = permission.LastUpdated,
                            UserName = String.Format("{0} {1}", employee.Name, employee.LastName),
                            PermissionGuid = permission.Guid,
                            PermissionName = permission.Name,
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
                List<PermissionsEmployee> permissionschange = queryPermissions.GetPermissionsExplicit(id);

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
                                unitOfWork.PermissionTypeRepository.Update(modifiedPermission);
                                unitOfWork.Save();
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
                    unitOfWork.PermissionTypeRepository.Insert(permType);
                    unitOfWork.Save();
                    unitOfWork.PermissionRepository.Insert(new PermissionsEmployee { Employees = new List<Employee>() { employee }, Guid = Guid.NewGuid(), LastUpdated = DateTime.UtcNow, PermissionTypes = permType });
                    unitOfWork.Save();
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
