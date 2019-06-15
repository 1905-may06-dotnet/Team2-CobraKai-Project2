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
        int AddJournal(); //returns 1 if successful, 0 if not
        Journal UpdateJournal(Journal journal);
        void DeleteJournal(int id);


        //IEnumerable<ListEntry> GetListEntries();


        //IEnumerable<MusicList> GetMusicLists();


        IEnumerable<Person> GetPersons();


        //IEnumerable<PlayList> GetPlayLists();


        IEnumerable<Song> GetSongs();
        Song GetSongById(int id);
        Song GetSongByTitle(string title);
        int CreateSong(); //return 1 if successful
        Song UpdateSong(Song song);
        void DeleteSong(int id);

        Person CreatePerson();
        //Person ReadPerson();
        Person UpdatePerson();
        Person DeletePerson();

/*        PlayList CreatePlayList();
        //PlayList ReadPlayList();
        PlayList UpdatePlayList();
        PlayList DeletePlayList();
*/


        int Save(); //returns 1 if successful, 0 if not
    }
}
