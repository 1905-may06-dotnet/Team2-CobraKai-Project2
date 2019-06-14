using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain
{
    public class Person
    {
        public Person()
        {
            Journals = new List<Journal>();
            Playlists = new List<PlayList>();
        }
        public int Id { get; set; }

        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public int MusicListId { get; set; }

        public MusicList MusicList { get; set; }
        public List<Journal> Journals { get; set; }
        public List<PlayList> Playlists { get; set; }
    }
}
