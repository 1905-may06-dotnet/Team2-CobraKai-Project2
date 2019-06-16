using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lib = Project.Domain;
using Project.Client.Entities;
using Project.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Project.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly Lib.IRepository repository;
        private List<Person> personList = new List<Person>();
        public PersonController(Lib.IRepository repository)
        {

            this.repository = repository;
        }
        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public ActionResult Get()
        {
            IEnumerable<Person> persons = Mapper.Map(repository.GetPersons());
            return Content(persons.ToList()[0].Firstname);

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}