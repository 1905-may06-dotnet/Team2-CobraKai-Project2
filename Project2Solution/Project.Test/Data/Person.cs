using NUnit.Framework;
using System.Collections.Generic;

namespace Project.Test
{
    public class Person
    {
        static Project.Domain.Person unitTest = new Project.Domain.Person();
        Project.Data.Entities.CobraKaiDbContext db = new Project.Data.Entities.CobraKaiDbContext();
        Project.Data.Repository a;

        [Test]
        public void A_Create()
        {
            a = new Project.Data.Repository(db);
            unitTest = new Project.Domain.Person();
            unitTest.Username = "unittest";
            a.CreatePerson(unitTest);
            Assert.Pass();
        }

        //test GetPersons()
        [Test]
        public void B_Read_1()
        {
            a = new Project.Data.Repository(db);
            IEnumerable<Project.Domain.Person> alist = a.GetPersons();
            Assert.Pass();
        }

        //test GetPersonbyId(int id)
        [Test]
        public void B_Read_2()
        {
            a = new Project.Data.Repository(db);
            int id = 0;
            foreach (var i in a.GetPersons())
            {
                id = i.Id;
            }
            unitTest = a.GetPersonById(id);
            Assert.AreEqual(unitTest.Email, null);
        }

        //test GetPersonbyUsername(string user)
        [Test]
        public void B_Read_3()
        {
            a = new Project.Data.Repository(db);
            int id = 0;
            foreach (var i in a.GetPersons())
            {
                id = i.Id;
            }
            unitTest = a.GetPersonByUsername("unittest");
            Assert.AreEqual(id, unitTest.Id);
        }

        [Test]
        public void C_Update()
        {
            a = new Project.Data.Repository(db);
            int id = 0;
            foreach (var i in a.GetPersons())
            {
                id = i.Id;
            }
            unitTest = a.GetPersonById(id);
            unitTest.Email = "a@b.c";
            a.UpdatePerson(unitTest);
            unitTest = a.GetPersonById(id);
            Assert.AreEqual(unitTest.Email, "a@b.c");
        }

        [Test]
        public void D_Delete()
        {
            a = new Project.Data.Repository(db);
            int id = 0;
            foreach (var i in a.GetPersons())
            {
                id = i.Id;
            }
            unitTest = new Project.Domain.Person();
            a.DeletePerson(id);
            Assert.Pass();
        }
    }
}
