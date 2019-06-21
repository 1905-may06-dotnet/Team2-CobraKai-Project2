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
        int CreateJournal(Journal journal); //returns 1 if successful, 0 if not
        int UpdateJournal(Journal journal);
        int DeleteJournal(int id);

        IEnumerable<Song> GetSongs();
        Song GetSongById(int id);
        Song GetSongByTitle(string title);
        int CreateSong(Song song); //return 1 if successful
        int UpdateSong(Song song);
        int DeleteSong(int id);

        IEnumerable<Person> GetPersons();
        Person GetPersonById(int id);
        Person GetPersonByUsername(string user);
        int CreatePerson(Person person); //returns 1 if successful
        int UpdatePerson(Person person);
        int DeletePerson(int id);


        IEnumerable<PlayList> GetPlayLists();
        PlayList GetPlayListById(int id);
        PlayList GetPlayListByTitle(string title);
        int CreatePlayList(PlayList playlist);
        int UpdatePlayList(PlayList playlist);
        int DeletePlayList(int id);


        int Save(); //returns 1 if successful, 0 if not
    }
}
