using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project.Data;

namespace Project.Test
{
    public class Person
    {
        static Project.Domain.Person unitTest = new Project.Domain.Person();
        Project.Data.Entities.CobraKaiDbContext db = new Project.Data.Entities.CobraKaiDbContext();
<<<<<<< HEAD

       
        [Test]
        public void A_Create()
        {
            Project.Data.Repository a = new Project.Data.Repository(db);
=======
        Project.Data.Repository a;

        [Test]
        public void A_Create()
        {
            a = new Project.Data.Repository(db);
>>>>>>> a4916f1fc4e046ca03a10be690b3f82e90edbc68
            unitTest = new Project.Domain.Person();
            a.CreatePerson(unitTest);
            Assert.Pass();
        }
<<<<<<< HEAD
        // [Test]
        // public void D_Delete()
        // {
        //     int personId = 3;
        //     Project.Data.Repository a = new Project.Data.Repository(db);
        //     unitTest = new Project.Domain.Person();
        //     a.DeletePerson(personId);
        //     Assert.Pass();
        // }
=======
        
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
            unitTest = a.GetPersonById(3);
            Assert.AreEqual(unitTest.Email, null);
        }

        [Test]
        public void C_Update()
        {
            a = new Project.Data.Repository(db);
            unitTest = a.GetPersonById(3);
            unitTest.Email = "a@b.c";
            a.UpdatePerson(unitTest);
            unitTest = a.GetPersonById(3);
            Assert.AreEqual(unitTest.Email, "a@b.c");
        }

        [Test]
        public void D_Delete()
        {
            int personId = 3;
            a = new Project.Data.Repository(db);
            unitTest = new Project.Domain.Person();
            a.DeletePerson(personId);
            Assert.Pass();
        }
>>>>>>> a4916f1fc4e046ca03a10be690b3f82e90edbc68
    }
}
