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
            try
            {
                Playlist plist = await Task.Run(() => Mapper.Map(repository.GetPlayListById(id)));
                return plist;
            }
            catch
            {
                return new Playlist();
            }
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(typeof(Playlist), StatusCodes.Status201Created)]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] Playlist plist)
        {
            if (!ModelState.IsValid) return BadRequest();

            int rowAffected = await Task.Run(() =>
            repository.CreatePlayList(Mapper.Map(plist)));

            if (rowAffected > 0) return Ok();

            return BadRequest();
        }


        //PUT api/values/5
        [HttpPut]
        [ProducesResponseType(typeof(Playlist), StatusCodes.Status202Accepted)]
        [Produces("application/json")]
        public async Task<IActionResult> Put([FromBody] Playlist plist)
        {
            if (!ModelState.IsValid) return BadRequest();

            int rowAffected = await Task.Run(() =>
            repository.UpdatePlayList(Mapper.Map(plist)));

            if (rowAffected > 0) return Ok();
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Playlist), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                int rowAffected = await Task.Run(() => repository.DeletePlayList(id));
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