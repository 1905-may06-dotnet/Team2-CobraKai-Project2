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
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace Project.Client.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly Lib.IRepository repository;

        public SongController(Lib.IRepository repository)
        {

            this.repository = repository;
        }
        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(Song), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IEnumerable<Song>> Get()
        {

            IEnumerable<Song> songs = await Task.Run(() => Mapper.Map(repository.GetSongs()));

            return songs;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Song), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<Song> Get([FromRoute]int id)
        {
            Song song = await Task.Run(() => Mapper.Map(repository.GetSongById(id)));

            return song;
        }

        // POST api/values
        [EnableCors]// it will use default policy
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Song song)
        {
            if (song != null)
            {
                int rowAffected = await Task.Run(() => repository.CreateSong(Mapper.Map(song)));

                return Created("", song);
            }

            return BadRequest();
        }


        //PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Song song)
        {
            int rowAffected = await Task.Run(() =>
            repository.UpdateSong(Mapper.Map(song)));

            if (rowAffected > 0) return Ok();
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            int rowAffected = await Task.Run(() => repository.DeleteSong(id));

            if (rowAffected > 0) return Ok();

            return BadRequest();
        }
    }
}