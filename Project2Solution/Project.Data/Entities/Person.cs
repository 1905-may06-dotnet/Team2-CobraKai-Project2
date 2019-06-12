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
        public Guid? MusicListId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Column("MusicList_Id")]
        public Guid? MusicListId1 { get; set; }

        [ForeignKey("MusicListId1")]
        [InverseProperty("People")]
        public virtual MusicList MusicListId1Navigation { get; set; }
        [InverseProperty("PersonId1Navigation")]
        public virtual ICollection<Journal> Journals { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}