using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Entities
{
    [Table("Songs", Schema = "CobraKai")]
    public partial class Song
    {
        public Song()
        {
            Journals = new HashSet<Journal>();
            ListEntries = new HashSet<ListEntry>();
        }

        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public string Genre { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Length { get; set; }
        [Required]
        public string ReleasedD { get; set; }

        [InverseProperty("SongId1Navigation")]
        public virtual ICollection<Journal> Journals { get; set; }
        [InverseProperty("Song")]
        public virtual ICollection<ListEntry> ListEntries { get; set; }
    }
}