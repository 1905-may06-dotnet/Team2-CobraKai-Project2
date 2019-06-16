using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain
{
    public class PlayList
    {
        //gets or sets playlist id
        public int Id { get; set; }
        //gets or sets user id
        //referenced from Person table
        public int? PersonId { get; set; }
        //gets or sets song id
        //referenced from Song table
        public int? SongId { get; set; }
        //gets or sets title of user's playlist
        //used to distinguish different playlists for single/multiple users
        public string Title { get; set; }

        //references Person table
        public Person Person { get; set; }
        //references list of songs in Song table under user's playlist
        public List<Song> Songs { get; set; }
    }
}
