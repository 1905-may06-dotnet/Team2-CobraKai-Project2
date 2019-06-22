using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;

namespace Project.Test.Client
{
    public class LogIn
    {
        Project.Data.Repository a;
        Project.Client.Entities.Person p;
        Project.Client.Controllers.LogInController li;
        Project.Data.Entities.CobraKaiDbContext db = new Project.Data.Entities.CobraKaiDbContext();
        

/*        [Test]
        public void LogIn()
        {
            p = new Project.Client.Entities.Person();
            p.Username = "Fred3";
            p.Password = "temp";
            
            var controller = new Project.Client.Controllers.LogInController(a);
            controller.Request = new HttpRequestMessage();
           
            Assert.AreEqual()
        }*/
    }
}
