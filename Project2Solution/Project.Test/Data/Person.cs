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

       
        [Test]
        public void A_Create()
        {
            Project.Data.Repository a = new Project.Data.Repository(db);
            unitTest = new Project.Domain.Person();
            a.CreatePerson(unitTest);
            Assert.Pass();
        }
        // [Test]
        // public void D_Delete()
        // {
        //     int personId = 3;
        //     Project.Data.Repository a = new Project.Data.Repository(db);
        //     unitTest = new Project.Domain.Person();
        //     a.DeletePerson(personId);
        //     Assert.Pass();
        // }
    }
}
