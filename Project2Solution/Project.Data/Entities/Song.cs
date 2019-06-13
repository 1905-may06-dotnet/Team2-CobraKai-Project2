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
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Size { get; set; }
        [Required]
        public string ReleaseDate { get; set; }
        public string FilePath { get; set; }

        [InverseProperty("Song")]
        public virtual ICollection<Journal> Journals { get; set; }
        [InverseProperty("Song")]
        public virtual ICollection<ListEntry> ListEntries { get; set; }
    }
}