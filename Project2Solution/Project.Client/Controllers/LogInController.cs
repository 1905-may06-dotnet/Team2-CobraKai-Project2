using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Client.Entities;
using Lib = Project.Domain;

namespace Project.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly Lib.IRepository repository;

        public LogInController(Lib.IRepository repository)
        {

            this.repository = repository;
        }

        /// <summary>
        /// Attempts to log user in.
        /// </summary>
        /// <param name="person"></param>
        /// <returns>200OK if successful, 401Unauthorized if unsuccessful</returns>
        public ActionResult LogIn([FromBody] Person person)
        {
            try
            {
                //retrieves user by username
                var user = repository.GetPersonByUsername(person.Username);
                //checks if password matches DB
                if (person.Password == user.Password) return Ok();
                //passwords do not match
                return Unauthorized();
            }
            catch
            {
                //user not found
                return Unauthorized();
            }
        }

        /// <summary>
        /// Attempts to create user.
        /// </summary>
        /// <param name="person"></param>
        /// <returns>201Created if successful, 400BadRequest if unsuccessful</returns>
        public ActionResult CreateAccount([FromBody] Person person)
        {
            var users = repository.GetPersons();
            foreach (var i in users)
            {
                //username is taken
                if (person.Username == i.Username) return BadRequest();
            }
            int value = repository.CreatePerson(Mapper.Map(person));

            if (value > 0) return Ok(); //change to created
            return BadRequest();
        }
    }
}