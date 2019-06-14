using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain
{
    public class ListEntry
    {
        public int Id { get; set; }

        public int SongId { get; set; }
        public int SongListId { get; set; }
        public int PlayListId { get; set; }
        public int MusicListId { get; set; }
        public int JournalId { get; set; }

        public bool Favorite { get; set; }
        public string TimeStamp { get; set; }

        public Journal Journal { get; set; }
        public PlayList MusicList { get; set; }
        public Song Song { get; set; }

        public MusicList SongList { get; set; }
    }
}   
