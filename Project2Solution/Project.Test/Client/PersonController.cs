using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using Project.Client;
using System.Collections.Generic;
using System.Linq;
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

        //private readonly List<Project.Client.Entities.Person> fakelist;

        //create dummy data
        /*public PersonController()
        {
            
        }*/


 /*       [Test]
        public async Task Get()
        {
            var testGet = GetTestPersons();
            var controller = new Project.Client.Controllers.PersonController(new Project.Data.Repository(db), testGet);

            var result = await controller.Get() as List<Person>;
            Assert.AreEqual(testGet, result);
        }*/

/*        private IEnumerable<Project.Client.Entities.Person> GetTestPersons()
        {
            IEnumerable<Project.Client.Entities.Person> testPersons;
            Project.Data.Repository a = new Project.Data.Repository(db);
            testPersons = Project.Client.Mapper.Map(a.GetPersons());
            
            return testPersons;
        }*/
    }
}
