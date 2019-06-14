using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Entities
{
    public partial class Person
    {
        public Person()
        {
            Journals = new List<Journal>();
            Playlists = new List<Playlist>();
        }

        public int Id { get; set; }
        public int MusicListId { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }

        public MusicList MusicList { get; set; }
        public List<Journal> Journals { get; set; }
        public List<Playlist> Playlists { get; set; }
    }
}