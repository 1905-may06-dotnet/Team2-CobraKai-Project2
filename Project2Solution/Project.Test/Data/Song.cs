using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project.Data;

namespace Project.Test
{
    public class Song
    {
        Project.Data.Entities.CobraKaiDbContext db = new Project.Data.Entities.CobraKaiDbContext();
   

       
        [Test]
        public void A_Create()
        {
            Project.Data.Repository repo = new Project.Data.Repository(db);
             Project.Domain.Song songTest = new Project.Domain.Song(){

                Title = "Encore",
                Artist = "Linkin Park",
                Genre = "Rock",
                Size = 3.45M,
                Length = "3.7",
                ReleaseDate = "1993",
                FilePath = "audio/file"
            };

            Assert.IsNotNull(songTest.Size);

            repo.CreateSong(songTest);
            Assert.Pass();
        }

        [Test]
        public void D_Delete()
        {
            int songId = 2;
            Project.Data.Repository repo = new Project.Data.Repository(db);
            repo.DeleteSong(songId);
            Assert.Pass();
        }

        [Test]
        public void A_Update()
        {
            Project.Domain.Song songTest = new Project.Domain.Song(){

                Id = 1,
                Title = "Encore",
                Artist = "Linkin Park",
                Genre = "Rock",
                Size = 3.45M,
                Length = "3.7",
                ReleaseDate = "1993",
                FilePath = "audio/file"
            };

            Project.Data.Repository repo = new Project.Data.Repository(db);
            repo.UpdateSong(songTest);

            Assert.Pass();
        }

        [Test]
        public void A_Read()
        {
             int songId = 1;
            Project.Data.Repository repo = new Project.Data.Repository(db);
            Project.Domain.Song song = repo.GetSongById(songId);

            string expectedValue = "Encore";
            string actualValue = song.Title;

            Assert.AreEqual(expectedValue, actualValue);
            //Assert.Pass();
        }
       
    }

      
}