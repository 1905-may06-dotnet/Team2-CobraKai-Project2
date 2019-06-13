using NUnit.Framework;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Test
{
    public class PlayList
    {
        static Project.Domain.Models.Elements.PlayList unitTest = new Project.Domain.Models.Elements.PlayList();

        [Test]
        public void A_Create()
        {
            unitTest = new Project.Domain.Models.Elements.PlayList();
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

 /*       [Test]
        public void D_Delete()
        {
            unitTest.Delete();

            using (var context = new Project.Data.CobraKaiDbContext())
                if (context.Playlists.Find(unitTest.Id) == null) Assert.Pass(); Assert.Fail();
        }*/
    }
}
