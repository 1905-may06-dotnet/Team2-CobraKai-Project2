using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Client.Entities
{
    public partial class Person
    {
        public Person()
        {
            // Journal = new HashSet<Journal>();
            // Playlist = new HashSet<Playlist>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // public virtual ICollection<Journal> Journal { get; set; }
        // public virtual ICollection<Playlist> Playlist { get; set; }
    }
}