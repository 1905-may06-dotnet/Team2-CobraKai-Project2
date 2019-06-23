using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Project.Client;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project.Test.Client
{

    public class PersonController
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public PersonController()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        /*public async Task<Project.Client.Entities.Person> TestGet()
        {
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            
            return null;
            
        }*/

        //Project.Client.Entities.Person p;
        //Project.Data.Entities.CobraKaiDbContext db = new Project.Data.Entities.CobraKaiDbContext();

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task Get()
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Person/");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }*/

    }
}
