using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Client.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lib = Project.Domain;

namespace Project.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly Lib.IRepository repository;

        IEnumerable<Person> plist = new List<Person>();

        public PersonController(Lib.IRepository repository)
        {

            this.repository = repository;
        }

        // GET api/values
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Produces("application/json")]
        public async Task<ActionResult<Person>> Get()
        {
            try
            {
                IEnumerable<Person> persons = await Task.Run(() => Mapper.Map(repository.GetPersons()));
                return CreatedAtAction(nameof(Get), persons);
            }
            catch
            {
                return NoContent(); //returns 204 status code
            }

        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<Person>> Get([FromRoute]int id)
        {
            try
            {
                Person person = await Task.Run(() => Mapper.Map(repository.GetPersonById(id)));
                return CreatedAtAction(nameof(Get), person);
            }
            catch
            {
                return NotFound(); //returns 404 status code, user does not exist
            }
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public async Task<IActionResult> Authenticate([FromBody] Person person)
        {
            if (person.Username == null || person.Username == "") return BadRequest(new { message = "Username or password is incorrect" });

            try
            {
                var user = await Task.Run(() => Mapper.Map(repository.GetPersonByUsername(person.Username)));
                if (user.Password == person.Password) return Ok();
                return Unauthorized();
            }
            catch
            {
                return BadRequest(new { message = "User not found" });
            }

        }


        // POST api/values
        [HttpPost]
        [ProducesResponseType(typeof(Person), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            if (!ModelState.IsValid) return BadRequest();

            foreach (var i in repository.GetPersons())
            {
                if (person.Username == i.Username) return Conflict(); //returns 409 error code if username is taken
            }

            int rowAffected = await Task.Run(() =>
            repository.CreatePerson(Mapper.Map(person)));

            if (rowAffected > 0) return Ok();

            return BadRequest();
        }

        //PUT api/values/5
        [HttpPut]
        [ProducesResponseType(typeof(Person), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Put([FromBody] Person person)
        {
            if (!ModelState.IsValid) return BadRequest();

            int rowAffected = await Task.Run(() =>
            repository.UpdatePerson(Mapper.Map(person)));

            if (rowAffected > 0) return Ok();
            return NoContent();
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();


                int rowAffected = await Task.Run(() => repository.DeletePerson(id));

                if (rowAffected > 0) return Ok();

                return BadRequest();
            }
            catch
            {
                return NoContent();
            }
        }
    }
}