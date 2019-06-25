using NUnit.Framework;
using System.Collections.Generic;

namespace Project.Test
{
    public class Journal
    {
        static Project.Domain.Journal unitTest = new Project.Domain.Journal();
        Project.Data.Entities.CobraKaiDbContext db = new Project.Data.Entities.CobraKaiDbContext();
        Project.Data.Repository a;

        [Test]
        public void A_Create()
        {
            a = new Project.Data.Repository(db);
            unitTest = new Project.Domain.Journal();
            unitTest.JournalEntry = "Test 1";
            unitTest.Title = "Test Title";
            a.CreateJournal(unitTest);
            Assert.Pass();
        }

        //test GetJournals()
        [Test]
        public void B_Read_1()
        {
            a = new Project.Data.Repository(db);
            IEnumerable<Project.Domain.Journal> alist = a.GetJournals();
            Assert.Pass();
        }

        //test GetJournalById(int id)
        [Test]
        public void B_Read_2()
        {
            a = new Project.Data.Repository(db);
            int jour = 0;
            foreach (var i in a.GetJournals())
            {
                jour = i.Id;
            }
            unitTest = a.GetJournalById(jour);
            Assert.AreEqual(unitTest.JournalEntry, "Test 1");
        }

        //test GetJournalByTitle(string title)
        [Test]
        public void B_Read_3()
        {
            a = new Project.Data.Repository(db);
            int jour = 0;
            foreach (var i in a.GetJournals())
            {
                jour = i.Id;
            }
            unitTest = a.GetJournalByTitle("Test Title");
            Assert.AreEqual(unitTest.Id, jour);
        }

        [Test]
        public void C_Update()
        {
            a = new Project.Data.Repository(db);
            int jour = 0;
            foreach (var i in a.GetJournals())
            {
                jour = i.Id;
            }
            unitTest = a.GetJournalById(jour);
            unitTest.JournalEntry = "Test new entry";
            a.UpdateJournal(unitTest);
            unitTest = a.GetJournalById(jour);
            Assert.AreEqual(unitTest.JournalEntry, "Test new entry");
        }

        [Test]
        public void D_Delete()
        {
            a = new Project.Data.Repository(db);
            int jour = 0;
            foreach (var i in a.GetJournals())
            {
                jour = i.Id;
            }
            a.DeleteJournal(jour);
            Assert.Pass();
        }
    }
}
