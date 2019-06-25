using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.Entities
{
    public partial class Person
    {
        public Person()
        {
            Journal = new HashSet<Journal>();
            Playlist = new HashSet<Playlist>();
        }

        public Person(string Email, string Firstname, string Lastname, string Username, string Password)
        {
            this.Email = Email;
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Username = Username;
            this.Password = Password;
        }

        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Journal> Journal { get; set; }
        public virtual ICollection<Playlist> Playlist { get; set; }
    }
}