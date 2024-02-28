using Infrastructure.Contexts;
using Infrastructure.ElasticViewModels;
using Microsoft.AspNetCore.Mvc;
using Queries.Interfaces;
using Queries.Implementation;
using Repository.Elastic;
using Repository.UnitOfWork;
using Serilog;
using Serilog.Sinks.File;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Challenge_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserElasticPermissionsController : ControllerBase
    {
        
        private UnitOfWorkPermissions unitOfWork;
        private UnitOfWorkElasticPermissions unitOfWorkElastic;
        private IQueryPermissions queryPermissions;
        private ChallengeContext context;
        private IQueryElasticPermissions<ViewModelElasticPermissionsUser> elasticQueries;
        public UserElasticPermissionsController(ChallengeContext contexto,
            IQueryPermissions queryPerms, UnitOfWorkElasticPermissions _unitOfWorkElastic, QueryElasticPermissions _elasticQueries)
        {
            unitOfWorkElastic = _unitOfWorkElastic;
            unitOfWork = new UnitOfWorkPermissions(contexto);
            queryPermissions = queryPerms;
            context = contexto;
            elasticQueries = _elasticQueries;
        }
        // GET: api/<UserElasticPermissionsController>
        [HttpGet]
        public List<ViewModelElasticPermissionsUser> Get()
        {
            var ruta = String.Format("{0} {1}://{2}{3}{4}", HttpContext.Request.Method, HttpContext.Request.Scheme, HttpContext.Request.Host, HttpContext.Request.Path, HttpContext.Request.QueryString);
            Log.Information("Consultando permisos de usuario por elastic search en la ruta {Ruta}",
                ruta);
            try
            {
                DateTime dateSubstract = DateTime.UtcNow.Subtract(TimeSpan.FromDays(7));
                return elasticQueries.GetPermissionsOrderedByDateUpdated("lastUpdated", dateSubstract.ToString());
            }
            catch (Exception ex)
            {
                Log.Error("Detalles del error {Err} en la ruta {Ruta}", ex.Message, ruta);
            }
            return null;
        }

        // GET api/<UserElasticPermissionsController>/5
        [HttpGet("{id}")]
        public List<ViewModelElasticPermissionsUser> Get(int id)
        {
            var ruta = String.Format("{0} {1}://{2}{3}{4}", HttpContext.Request.Method, HttpContext.Request.Scheme, HttpContext.Request.Host, HttpContext.Request.Path, HttpContext.Request.QueryString);
            try
            {
                Log.Information("Consultando permisos de usuario por elastic search en la ruta {Ruta}",
                ruta);
                DateTime dateSubstract = DateTime.UtcNow.Subtract(TimeSpan.FromDays(7));
                return elasticQueries.GetPermissionsOrderedByDateUpdated("lastUpdated", dateSubstract.ToString()).Where(a => a.UserId == id).ToList();
            }
            catch(Exception ex)
            {
                Log.Error("Detalles del error {Err} en la ruta {Ruta}", ex.Message, ruta);
            }
            return null;
        }

        // POST api/<UserElasticPermissionsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UserElasticPermissionsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UserElasticPermissionsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
