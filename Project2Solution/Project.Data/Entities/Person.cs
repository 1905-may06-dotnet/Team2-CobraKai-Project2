using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Entities
{
    [Table("People", Schema = "CobraKai")]
    public partial class Person
    {
        public Person()
        {
            Journals = new HashSet<Journal>();
            Playlists = new HashSet<Playlist>();
        }

        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Column("MusicList_Id")]
        public Guid? MusicListId { get; set; }

        [ForeignKey("MusicListId")]
        [InverseProperty("People")]
        public virtual MusicList MusicList { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<Journal> Journals { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}