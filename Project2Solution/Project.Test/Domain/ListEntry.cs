using NUnit.Framework;
using System.Linq;
using Scaffold;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scaffold.Test.Domain {

    public class ListEntry {

        static Project.Domain.Models.Elements.ListEntry unitTest = new Project.Domain.Models.Elements.ListEntry ();

        [Test]
        public void A_Create () {

            unitTest = new Project.Domain.Models.Elements.ListEntry ();

            unitTest.Save ();

            Assert.Pass ();

        }

        [Test]
        public void B_Read () {

            var query = new Project.Domain.Models.Elements.QueryListEntry { Id = unitTest.Id };
            var listEntries = new Project.Domain.Models.ListEntries ();

            if ( listEntries.Query ( ref query ).Id == unitTest.Id ) Assert.Pass (); else Assert.Fail ();

            if ( ( from rec in listEntries.Records where rec.Id == query.Id select rec ).Any () ) Assert.Pass (); else Assert.Fail ();

        }

        [Test]
        public void C_Update () {

            unitTest.Favorite     = "Update/Read Test";
            unitTest.JournalEntry = "Update/Read Test";

            unitTest.Save ();

            Assert.Pass ();

        }

       [Test]
        public void D_Delete () {

            unitTest.Delete ();

             using ( var context = new Project.Data.CobraKaiDbContext () )
                if ( context.ListEntries.Find ( unitTest.Id ) == null ) Assert.Pass (); Assert.Fail ();

        }

    }

}
