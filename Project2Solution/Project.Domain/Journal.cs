using System;
using System.Collections.Generic;

namespace Project.Domain
{
    public class Journal
    {
        //gets or sets Journal ID
        public int Id { get; set; }

        //gets or sets User's ID
        //referenced from Person table
        public int? PersonId { get; set; }
        //gets or sets Song's ID
        //referenced from Song table
        public int? SongId { get; set; }
        //gets or sets Title of Journal
        public string Title { get; set; }
        //gets or sets Journal Entry for Song
        public string JournalEntry { get; set; }

        //references Person table
        public Person Person { get; set; }
        //references list of songs in any of user's playlists
        public List<Song> Song { get; set; }
    }
}
