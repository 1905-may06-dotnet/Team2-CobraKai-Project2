using Project.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Data
{
    public class Repository : IRepository
    {
        
        public Repository() {

        }
        public int AddJournal()
        {
            throw new NotImplementedException();
        }

        public Person CreatePerson()
        {
            throw new NotImplementedException();
        }

        public int CreateSong()
        {
            throw new NotImplementedException();
        }

        public void DeleteJournal(int id)
        {
            throw new NotImplementedException();
        }

        public Person DeletePerson()
        {
            throw new NotImplementedException();
        }

        public void DeleteSong(int id)
        {
            throw new NotImplementedException();
        }

        public Journal GetJournalById(int id)
        {
            throw new NotImplementedException();
        }

        public Journal GetJournalByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Journal> GetJournals()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetPersons()
        {
            throw new NotImplementedException();
        }

        public Song GetSongById(int id)
        {
            throw new NotImplementedException();
        }

        public Song GetSongByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Song> GetSongs()
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public Journal UpdateJournal(Journal journal)
        {
            throw new NotImplementedException();
        }

        public Person UpdatePerson()
        {
            throw new NotImplementedException();
        }

        public Song UpdateSong(Song song)
        {
            throw new NotImplementedException();
        }
    }
}
