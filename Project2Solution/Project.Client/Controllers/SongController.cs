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
            try
            {
                Song song = await Task.Run(() => Mapper.Map(repository.GetSongById(id)));
                return song;
            }
            catch
            {
                return new Song();
            }
        }

        // POST api/values
        [EnableCors]// it will use default policy
        [HttpPost]
        [ProducesResponseType(typeof(Song), StatusCodes.Status201Created)]
        [Produces("application/json")]
        public IActionResult Post([FromBody] Song song)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (song != null)
            {
                return Ok();
                //var songT = await Task.Run(() => repository.GetSongByTitle(song.Title));

                //if(songT == null)
                //{
                //    int rowAffected = await Task.Run(() => repository.CreateSong(Mapper.Map(song)));

                //    return Created("", song);
                //}else
                //{
                //    return null;
                //}

            }

            return BadRequest();
        }


        //PUT api/values/5
        [HttpPut]
        [ProducesResponseType(typeof(Song), StatusCodes.Status202Accepted)]
        [Produces("application/json")]
        public async Task<IActionResult> Put([FromBody] Song song)
        {
            if (!ModelState.IsValid) return BadRequest();

            int rowAffected = await Task.Run(() =>
            repository.UpdateSong(Mapper.Map(song)));

            if (rowAffected > 0) return Ok();
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Song), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                int rowAffected = await Task.Run(() => repository.DeleteSong(id));

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
