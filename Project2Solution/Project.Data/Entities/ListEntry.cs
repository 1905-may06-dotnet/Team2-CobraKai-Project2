using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Entities
{
    public partial class ListEntry
    {
        public int Id { get; set; }
        public int PlayListId { get; set; }
        public int MusicListId { get; set; }
        public bool Favorite { get; set; }
        public string TimeStamp { get; set; }
        public string JournalEntry { get; set; }
        public int SonglistId { get; set; }
        public int SongId { get; set; }
        public int JournalId { get; set; }

        public Journal Journal { get; set; }

        public Playlist MusicList { get; set; }

        public Song Song { get; set; }

        public MusicList Songlist { get; set; }
    }
}