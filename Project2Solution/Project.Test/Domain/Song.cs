using NUnit.Framework;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Test
{
    public class Song
    {
        static Project.Domain.Models.Elements.Song unitTest = new Project.Domain.Models.Elements.Song();

        [Test]
        public void A_Create()
        {
            unitTest = new Project.Domain.Models.Elements.Song();
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
                if (context.Songs.Find(unitTest.Id) == null) Assert.Pass(); Assert.Fail();
        }
    }
}
