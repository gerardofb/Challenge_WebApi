using Infrastructure.Contexts;
using Infrastructure.ElasticViewModels;
using Microsoft.AspNetCore.Mvc;
using Queries.Interfaces;
using Repository.Elastic;
using Repository.UnitOfWork;

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
        private ElasticRepositoryPermissions<ViewModelElasticPermissionsUser> elasticRepository;
        public UserElasticPermissionsController(ChallengeContext contexto,
            IQueryPermissions queryPerms, UnitOfWorkElasticPermissions _unitOfWorkElastic)
        {
            unitOfWorkElastic = _unitOfWorkElastic;
            unitOfWork = new UnitOfWorkPermissions(contexto);
            queryPermissions = queryPerms;
            context = contexto;
        }
        // GET: api/<UserElasticPermissionsController>
        [HttpGet]
        public List<ViewModelElasticPermissionsUser> Get()
        {
            DateTime dateSubstract = DateTime.UtcNow.Subtract(TimeSpan.FromDays(7));
            return unitOfWorkElastic.ElasticRepositoryPermissions.GetPermissionsOrderedByDateUpdated("lastUpdated", dateSubstract.ToString());
        }

        // GET api/<UserElasticPermissionsController>/5
        [HttpGet("{id}")]
        public List<ViewModelElasticPermissionsUser> Get(int id)
        {
            DateTime dateSubstract = DateTime.UtcNow.Subtract(TimeSpan.FromDays(7));
            return unitOfWorkElastic.ElasticRepositoryPermissions.GetPermissionsOrderedByDateUpdated("lastUpdated", dateSubstract.ToString()).Where(a=> a.UserId == id).ToList();
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
