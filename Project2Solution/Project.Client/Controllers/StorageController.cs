using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Client.Entities;
using Lib = Project.Domain;

namespace Project.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly Lib.IRepository repository;

        public StorageController(Lib.IRepository repository)
        {

            this.repository = repository;
        }

        [HttpPost, DisableRequestSizeLimit]

        public  async Task<IActionResult> Post()
        {
            //try
            //{
                var files = Request.Form.Files;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                ms.ToArray();
                            //string s = Convert.ToBase64String(fileBytes);

                            if (MediaServicesClient.Client == null) await Task.Run(() => MediaServicesClient.Connect());
                            await Task.Run(() => MediaServicesClient.Upload(ms, file.FileName));

                            return Ok(file.FileName);

                        }
                        }
                    }
                }
                return Ok();

                //}catch(Exception ex)
                //{
                //return BadRequest(ex);
                //}
            

        
        }

        //[HttpGet("{title}")]
        //[Produces("application/json")]
        //public async Task<Song> Get([FromRoute]string title)
        //{
        //    try
        //    {

        //        //vanguardmediaservice.
        //        //return song;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
    }
}
