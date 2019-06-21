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
    public class PlayListController : ControllerBase
    {
        private readonly Lib.IRepository repository;

        public PlayListController(Lib.IRepository repository)
        {

            this.repository = repository;
        }
        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(Playlist), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IEnumerable<Playlist>> Get()
        {

            IEnumerable<Playlist> plists = await Task.Run(() => Mapper.Map(repository.GetPlayLists()));

            return plists;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Playlist), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<Playlist> Get([FromRoute]int id)
        {
            Playlist plist = await Task.Run(() => Mapper.Map(repository.GetPlayListById(id)));

            return plist;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Playlist plist)
        {
            if (ModelState.IsValid) return BadRequest();

            int rowAffected = await Task.Run(() =>
            repository.CreatePlayList(Mapper.Map(plist)));

            if (rowAffected > 0) return Ok();

            return BadRequest();
        }


        //PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Playlist plist)
        {
            int rowAffected = await Task.Run(() =>
            repository.UpdatePlayList(Mapper.Map(plist)));

            if (rowAffected > 0) return Ok();
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            int rowAffected = await Task.Run(() => repository.DeletePerson(id));

            if (rowAffected > 0) return Ok();

            return BadRequest();
        }
    }
}