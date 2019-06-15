using Project.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Data
{
    public class Repository : IRepository
    {
        private readonly Entities.CobraKaiDbContext _db;

        public Repository(Entities.CobraKaiDbContext db)
        {
            _db = db;
        }
        //adds new journal
        public void CreateJournal(Project.Domain.Journal journal)
        {
            _db.Journals.Add(Mapper.Map(journal));
            Save();
        }
        //adds new user
        public void CreatePerson(Project.Domain.Person person)
        {
            _db.People.Add(Mapper.Map(person));
            Save();
        }
        //adds new playlist
        public void CreatePlayList(Project.Domain.PlayList playlist)
        {
            _db.Playlists.Add(Mapper.Map(playlist));
            Save();
        }
        //adds new song
        public void CreateSong(Project.Domain.Song song)
        {
            _db.Songs.Add(Mapper.Map(song));
            Save();
        }
        //deletes journal
        public void DeleteJournal(int id)
        {
            _db.Remove(_db.Journals.Find(id));
            Save();
        }
        //deletes user
        public void DeletePerson(int id)
        {
            _db.Remove(_db.People.Find(id));
            Save();
        }
        //deletes playlist
        public void DeletePlayList(int id)
        {
            _db.Remove(_db.Playlists.Find(id));
            Save();
        }
        //deletes song
        public void DeleteSong(int id)
        {
            _db.Remove(_db.Songs.Find(id));
            Save();
        }
        //gets journal by journal id
        public Journal GetJournalById(int id)
        {
            var jour = _db.Journals.Where(j => j.Id == id).FirstOrDefault();
            return Mapper.Map(jour);
        }
        //gets journal by title of journal
        public Journal GetJournalByTitle(string title)
        {
            var jour = _db.Journals.Where(j => j.Title == title).FirstOrDefault();
            return Mapper.Map(jour);
        }
        //gets all journals
        public IEnumerable<Journal> GetJournals()
        {
            return _db.Journals.Select(j => Mapper.Map(j));
        }
        //gets user by user id
        public Person GetPersonById(int id)
        {
            var per = _db.People.Where(p => p.Id == id).FirstOrDefault();
            return Mapper.Map(per);
        }
        //gets all users
        public IEnumerable<Person> GetPersons()
        {
            return _db.People.Select(p => Mapper.Map(p));
        }
        //gets playlist by playlist id
        public PlayList GetPlayListById(int id)
        {
            var plist = _db.Playlists.Where(pl => pl.Id == id).FirstOrDefault();
            return Mapper.Map(plist);
        }
        //gets playlist by title of playlist
        public PlayList GetPlayListByTitle(string title)
        {
            var plist = _db.Playlists.Where(pl => pl.Title == title).FirstOrDefault();
            return Mapper.Map(plist);
        }
        //gets all playlists
        public IEnumerable<PlayList> GetPlayLists()
        {
            return _db.Playlists.Select(pl => Mapper.Map(pl));
        }
        //gets song by song id
        public Song GetSongById(int id)
        {
            var song = _db.Songs.Where(s => s.Id == id).FirstOrDefault();
            return Mapper.Map(song);
        }
        //gets song by title of song
        public Song GetSongByTitle(string title)
        {
            var song = _db.Songs.Where(s => s.Title == title).FirstOrDefault();
            return Mapper.Map(song);
        }
        //gets all songs
        public IEnumerable<Song> GetSongs()
        {
            return _db.Songs.Select(s => Mapper.Map(s));
        }
        //updates journal
        public void UpdateJournal(Journal journal)
        {
            _db.Entry(_db.Journals.Find(journal.Id)).CurrentValues.SetValues(Mapper.Map(journal));
            Save();
        }
        //updates user
        public void UpdatePerson(Person person)
        {
            _db.Entry(_db.People.Find(person.Id)).CurrentValues.SetValues(Mapper.Map(person));
            Save();
        }
        //updates playlist
        public void UpdatePlayList(PlayList playlist)
        {
            _db.Entry(_db.Playlists.Find(playlist.Id)).CurrentValues.SetValues(Mapper.Map(playlist));
            Save();
        }
        //updates song
        public void UpdateSong(Song song)
        {
            _db.Entry(_db.Songs.Find(song.Id)).CurrentValues.SetValues(Mapper.Map(song));
            Save();
        }
        //saves changes in database
        public int Save()
        {
            return _db.SaveChanges();
        }
    }
}
