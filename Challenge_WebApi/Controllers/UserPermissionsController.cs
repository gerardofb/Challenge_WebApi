using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Implementation;
using Queries.Interfaces;
using Infrastructure.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Challenge_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionsController : ControllerBase
    {
        private IRepositoryEmployee repositoryEmployee;
        private IRepositoryPermissionsEmployee repositoryPermissions;
        private IQueryPermissions queryPermissions;
        public UserPermissionsController(IRepositoryEmployee repositoryemp, IRepositoryPermissionsEmployee repositoryperm, IQueryPermissions queryPerms)
        {
            repositoryEmployee = repositoryemp;
            repositoryPermissions = repositoryperm;
            queryPermissions = queryPerms;
        }
        // GET: api/<ChallengeController>
        [HttpGet("{id}")]
        public IEnumerable<string> Get(int Id)
        {
            throw new NotImplementedException();
        }

        // GET api/<ChallengeController>/5
        [HttpGet]
        public List<Employee> Get(string Name)
        {
            List<Employee> employees = null;
            try
            {
                employees = queryPermissions.Get(Name);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return employees;
        }

        // POST api/<ChallengeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ChallengeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChallengeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
