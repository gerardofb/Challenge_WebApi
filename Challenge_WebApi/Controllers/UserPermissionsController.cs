using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.UnitOfWork;
using Queries.Interfaces;
using Infrastructure.Models;
using Infrastructure.Contexts;
using Challenge_WebApi.ViewModel;
using Nest;
using Repository.Elastic;
using Infrastructure.ElasticViewModels;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Challenge_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionsController : ControllerBase
    {
        private UnitOfWorkPermissions unitOfWork;
        private UnitOfWorkElasticPermissions unitOfWorkElastic;
        private IQueryPermissions queryPermissions;
        private ChallengeContext context;
        private ElasticRepositoryPermissions<ViewModelElasticPermissionsUser> elasticRepository;
        public UserPermissionsController(ChallengeContext contexto,
            IQueryPermissions queryPerms, UnitOfWorkElasticPermissions _unitOfWorkElastic)
        {
            unitOfWorkElastic = _unitOfWorkElastic;
            unitOfWork = new UnitOfWorkPermissions(contexto);
            queryPermissions = queryPerms;
            context = contexto;
        }
        // GET: api/<ChallengeController>

        [HttpGet("GetPermissions/{id}")]
        public IEnumerable<ViewModelPermissionsUser> GetPermissions(int id)
        {
            var ruta = String.Format("{0} {1}//{2}{3}{4}", HttpContext.Request.Method, HttpContext.Request.Scheme, HttpContext.Request.Host, HttpContext.Request.Path, HttpContext.Request.QueryString);
            Log.Information("Consultando permisos de usuario en la ruta {Ruta}", ruta);
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
                Log.Error("Detalles del error {Err} en la ruta {Ruta}", ex.Message, ruta);
            }
            return (salida != null && salida.Count > 0) ? salida : null;
        }

        // GET api/<ChallengeController>/5
        [HttpGet("GetUser/{name}")]
        public List<ViewModelUser> GetUser(string name)
        {
            var ruta = String.Format("{0} {1}//{2}{3}{4}", HttpContext.Request.Method, HttpContext.Request.Scheme, HttpContext.Request.Host, HttpContext.Request.Path, HttpContext.Request.QueryString);
            Log.Information("Consultando permisos de usuario por elastic search en la ruta {Ruta}", ruta);
            List<Employee> employees = null;
            List<ViewModelUser> users = null;
            try
            {
                employees = queryPermissions.Get(Uri.UnescapeDataString(name));
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
                Log.Error("Detalles del error {Err} en la ruta {Ruta}", ex.Message, ruta);
            }
            return users.Count > 0 ? users : null;
        }

        // POST api/<ChallengeController>
        [HttpPost("ModifyPermission/{id}")]
        public ObjectResult Post(int id, [FromBody] ViewModelChangePermission newPermissions)
        {
            var ruta = String.Format("{0} {1}//{2}{3}{4}", HttpContext.Request.Method, HttpContext.Request.Scheme, HttpContext.Request.Host, HttpContext.Request.Path, HttpContext.Request.QueryString);
            Log.Information("Modificando permisos de usuario por en la ruta {Ruta}", ruta);
            try
            {

                Employee employee = queryPermissions.Get(new Employee { Id = id });
                List<PermissionsEmployee> permissionschange = queryPermissions.GetPermissionsExplicit(id);

                if (employee != null && permissionschange.Count > 0)
                {

                    IEnumerable<string> oldPermissions = from a in permissionschange
                                                         select a.PermissionTypes != null ?
                                                         a.PermissionTypes.Name : "";
                    if (oldPermissions.Any(x => x == newPermissions.OldPermission))
                    {

                        PermissionType modifiedPermission = permissionschange.Select(d => d.PermissionTypes)
                            .First(a => a.Name == newPermissions.OldPermission);

                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                if (ModelState.IsValid)
                                {
                                    modifiedPermission.CreatedDate = DateTime.UtcNow;
                                    modifiedPermission.Name = newPermissions.NewPermission.Trim();
                                    unitOfWork.PermissionTypeRepository.Update(modifiedPermission);
                                    unitOfWork.Save();
                                    transaction.Commit();
                                    // GUARDADO DE ENTIDAD EN ELASTIC SEARCH
                                    PermissionsEmployee permission = permissionschange.First(a => a.PermissionTypes.Name == newPermissions.NewPermission);
                                    ViewModelElasticPermissionsUser permissionUserElastic = unitOfWorkElastic.ElasticRepositoryPermissions.InsertPriorPermissions(new ViewModelElasticPermissionsUser
                                    {
                                        LastUpdated = DateTime.UtcNow,
                                        PermissionGuid = permission.Guid,
                                        PermissionName = newPermissions.NewPermission,
                                        UserId = employee.Id,
                                        UserName = String.Format("{0} {1}", employee.Name, employee.LastName)
                                    }).Result;
                                    if (permissionUserElastic == null)
                                    {
                                        throw new Exception("No fue posible guardar en el índice de elastic search");
                                    }
                                    // FINALIZA GUARDADO DE ENTIDAD EN ELASTIC SEARCH

                                }
                                else return BadRequest(String.Join(',', ModelState.Values.Where(a => a.Errors.Count > 0)));
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Detalles del error {Err} en la ruta {Ruta}", ex.Message, ruta);
                                transaction.Rollback();
                                return BadRequest("An unexpected error ocurred");
                            }
                        }
                        return Ok("Changes commited");
                    }

                }
                return NotFound("Could not find a permission to change");
            }
            catch (Exception ex)
            {
                Log.Error("Detalles del error {Err} en la ruta {Ruta}", ex.Message, ruta);
                return BadRequest("An unexpected error ocurred");
            }
        }

        // PUT api/<ChallengeController>/5
        [HttpPut("RequestPermission/{id}")]
        public StatusCodeResult RequestPermission(int id, [FromBody] string permissionValue)
        {
            var ruta = String.Format("{0} {1}//{2}{3}{4}", HttpContext.Request.Method, HttpContext.Request.Scheme, HttpContext.Request.Host, HttpContext.Request.Path, HttpContext.Request.QueryString);
            Log.Information("Agregando permisos de usuario por en la ruta {Ruta}", ruta);
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Employee employee = unitOfWork.EmployeeRepository.GetById(id);
                    PermissionType permType = new PermissionType() { Name = permissionValue, CreatedDate = DateTime.UtcNow };
                    unitOfWork.PermissionTypeRepository.Insert(permType);
                    unitOfWork.Save();
                    PermissionsEmployee newpermission = new PermissionsEmployee { Employees = new List<Employee>() { employee }, Guid = Guid.NewGuid(), LastUpdated = DateTime.UtcNow, PermissionTypes = permType };
                    unitOfWork.PermissionRepository.Insert(newpermission);
                    unitOfWork.Save();
                    // GUARDADO DE ENTIDAD EN ELASTIC SEARCH
                    PermissionsEmployee permission = unitOfWork.PermissionRepository.Get(a => a.Guid == newpermission.Guid).First();
                    ViewModelElasticPermissionsUser permissionUserElastic = unitOfWorkElastic.ElasticRepositoryPermissions.InsertPriorPermissions(new ViewModelElasticPermissionsUser
                    {
                        LastUpdated = permission.LastUpdated,
                        PermissionGuid = permission.Guid,
                        PermissionName = permType.Name,
                        UserId = employee.Id,
                        UserName = String.Format("{0} {1}", employee.Name, employee.LastName)
                    }).Result;
                    if (permissionUserElastic == null)
                    {
                        throw new Exception("No fue posible guardar en el índice de elastic search");
                    }
                    // FINALIZA GUARDADO DE ENTIDAD EN ELASTIC SEARCH
                    transaction.Commit();
                    return Ok();
                }
                catch (NotImplementedException ex)
                {
                    transaction.Rollback();
                    Log.Error("Detalles del error {Err} en la ruta {Ruta}", ex.Message, ruta);
                    return NotFound();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Log.Error("Detalles del error {Err} en la ruta {Ruta}", ex.Message, ruta);
                    return BadRequest();
                }

            }
        }

        // DELETE api/<ChallengeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var ruta = String.Format("{0} {1}//{2}{3}{4}", HttpContext.Request.Method, HttpContext.Request.Scheme, HttpContext.Request.Host, HttpContext.Request.Path, HttpContext.Request.QueryString);
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Log.Error("Detalles del error {Err} en la ruta {Ruta}", ex.Message, ruta);
                throw ex;
            }
        }
    }
}
