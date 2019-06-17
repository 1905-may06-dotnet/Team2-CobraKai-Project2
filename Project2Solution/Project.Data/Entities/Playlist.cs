using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Entities
{
    public partial class Playlist
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int? PersonId { get; set; }
        [ForeignKey("Song")]
        public int? SongId { get; set; }
        public string Title { get; set; }

        public virtual Person Person { get; set; }
        public virtual Song Song { get; set; }
    }
}