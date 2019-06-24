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
using Microsoft.AspNetCore.Cors;

namespace Project.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly Lib.IRepository repository;
     
        public PersonController(Lib.IRepository repository)
        {

            this.repository = repository;
        }
        // GET api/values
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IEnumerable<Person>> Get()
        {
        
            IEnumerable<Person> persons = await Task.Run(() => Mapper.Map(repository.GetPersons()));

            return persons;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<Person> Get([FromRoute]int id)
        {
            Person person = await Task.Run(() => Mapper.Map(repository.GetPersonById(id)));

            return person;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            if (ModelState.IsValid) return BadRequest();

            int rowAffected = await Task.Run(() =>
            repository.CreatePerson(Mapper.Map(person)));

            if (rowAffected > 0) return Ok();

            return BadRequest();
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
           int rowAffected = await Task.Run(()=> repository.DeletePerson(id));

            if(rowAffected > 0) return Ok();

            return BadRequest();
        }
    }
}
