using NUnit.Framework;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Test
{
    public class Person
    {
        static Project.Domain.Models.Elements.Person unitTest = new Project.Domain.Models.Elements.Person();

        [Test]
        public void A_Create()
        {
            unitTest = new Project.Domain.Models.Elements.Person();
            unitTest.Save();
            Assert.Pass();
        }

        /*        [Test]
                public void B_Read()
                {

                }*/

        /*       public void C_Update()
               {

               }*/

        [Test]
        public void D_Delete()
        {
            unitTest.Delete();

            using (var context = new Project.Data.CobraKaiDbContext())
                if (context.People.Find(unitTest.Id) == null) Assert.Pass(); Assert.Fail();
        }
    }
}
