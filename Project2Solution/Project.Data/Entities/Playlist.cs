using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Entities
{
    [Table("Playlists", Schema = "CobraKai")]
    public partial class Playlist
    {
        public Playlist()
        {
            ListEntries = new HashSet<ListEntry>();
        }

        public Guid Id { get; set; }
        public Guid? PersonId { get; set; }
        [Required]
        public string Title { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("Playlists")]
        public virtual Person Person { get; set; }
        [InverseProperty("MusicList")]
        public virtual ICollection<ListEntry> ListEntries { get; set; }
    }
}