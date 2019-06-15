using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project.Data;

namespace Project.Test
{
    public class Playlist
    {
        static Project.Domain.PlayList unitTest = new Project.Domain.PlayList();
        Project.Data.Entities.CobraKaiDbContext db = new Project.Data.Entities.CobraKaiDbContext();
        Project.Data.Repository a;

        [Test]
        public void A_Create()
        {
            a = new Project.Data.Repository(db);
            unitTest = new Project.Domain.PlayList();
            unitTest.Title = "testing";
            a.CreatePlayList(unitTest);
            Assert.Pass();
        }

        //test GetPlayLists()
        [Test]
        public void B_Read_1()
        {
            a = new Project.Data.Repository(db);
            IEnumerable<Project.Domain.PlayList> alist = a.GetPlayLists();
            Assert.Pass();
        }

        //test GetPlayListById(int id)
        [Test]
        public void B_Read_2()
        {
            a = new Project.Data.Repository(db);
            int pl = 0;
            foreach (var i in a.GetPlayLists())
            {
                pl = i.Id;
            }
            unitTest = a.GetPlayListById(pl);
            Assert.AreEqual(unitTest.Id, pl);
        }

        //test GetPlayListByTitle(string title)
        [Test]
        public void B_Read_3()
        {
            a = new Project.Data.Repository(db);
            int pl = 0;
            foreach (var i in a.GetPlayLists())
            {
                pl = i.Id;
            }
            unitTest = a.GetPlayListByTitle("testing");
            Assert.AreEqual(unitTest.Id, pl);
        }

        [Test]
        public void C_Update()
        {
            a = new Project.Data.Repository(db);
            int pl = 0;
            foreach (var i in a.GetPlayLists())
            {
                pl = i.Id;
            }
            unitTest = a.GetPlayListById(pl);
            unitTest.Title = "newtitle";
            a.UpdatePlayList(unitTest);
            unitTest = a.GetPlayListById(pl);
            Assert.AreEqual(unitTest.Title, "newtitle");
        }

        [Test]
        public void D_Delete()
        {
            int pl = 0;
            foreach (var i in a.GetPlayLists())
            {
                pl = i.Id;
            }
            a = new Project.Data.Repository(db);
            a.DeletePlayList(pl);
            Assert.Pass();
        }
    }
}
