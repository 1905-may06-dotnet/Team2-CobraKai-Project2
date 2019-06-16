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
            Project.Domain.Song songTest = new Project.Domain.Song()
            {

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

        public void B_Read_1()
        {
            Project.Data.Repository repo = new Project.Data.Repository(db);
            repo.GetSongs();
            Assert.Pass();
        }

        [Test]
        public void B_Read_2()
        {
            int songId = 0;
            Project.Data.Repository repo = new Project.Data.Repository(db);
            foreach (var i in repo.GetSongs())
            {
                songId = i.Id;
            }
            Project.Domain.Song song = repo.GetSongById(songId);

            string expectedValue = "Encore";
            string actualValue = song.Title;

            Assert.AreEqual(expectedValue, actualValue);
            //Assert.Pass();
        }

        [Test]
        public void B_Read_3()
        {
            int songId = 0;
            Project.Data.Repository repo = new Project.Data.Repository(db);
            foreach (var i in repo.GetSongs())
            {
                songId = i.Id;
            }
            Project.Domain.Song song = repo.GetSongByTitle("Encore");
            Assert.AreEqual(song.Id, songId);
        }

        [Test]
        public void C_Update()
        {
            Project.Data.Repository repo = new Project.Data.Repository(db);
            int songid = 0;
            foreach (var i in repo.GetSongs()) {
                songid = i.Id;
            }
            Project.Domain.Song songTest = new Project.Domain.Song()
            {

                Id = songid,
                Title = "Encore",
                Artist = "Linkin Park",
                Genre = "Rock",
                Size = 3.45M,
                Length = "3.7",
                ReleaseDate = "1993",
                FilePath = "audio/file"
            };

            
            repo.UpdateSong(songTest);

            Assert.Pass();
        }


        [Test]
        public void D_Delete()
        {
            int songId = 0;
            Project.Data.Repository repo = new Project.Data.Repository(db);
            foreach (var i in repo.GetSongs())
            {
                songId = i.Id;
            }
            repo.DeleteSong(songId);
            Assert.Pass();
        }

    }


}