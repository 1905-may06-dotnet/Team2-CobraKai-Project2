using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain
{
    public interface IRepository
    {
        IEnumerable<Journal> GetJournals();
        Journal GetJournalById(int id);
        Journal GetJournalByTitle(string title);
        void CreateJournal(Journal journal); //returns 1 if successful, 0 if not
        void UpdateJournal(Journal journal);
        void DeleteJournal(int id);

        IEnumerable<Song> GetSongs();
        Song GetSongById(int id);
        Song GetSongByTitle(string title);
        int CreateSong(Song song); //return 1 if successful
        int UpdateSong(Song song);
        int DeleteSong(int id);

        IEnumerable<Person> GetPersons();
        Person GetPersonById(int id);
        int CreatePerson(Person person); //returns 1 if successful
        int UpdatePerson(Person person);
        int DeletePerson(int id);

        IEnumerable<PlayList> GetPlayLists();
        PlayList GetPlayListById(int id);
        PlayList GetPlayListByTitle(string title);
        void CreatePlayList(PlayList playlist);
        void UpdatePlayList(PlayList playlist);
        void DeletePlayList(int id);


        int Save(); //returns 1 if successful, 0 if not
    }
}
