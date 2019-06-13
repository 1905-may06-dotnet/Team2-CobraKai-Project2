using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Entities
{
    [Table("Journals", Schema = "CobraKai")]
    public partial class Journal
    {
        public Journal()
        {
            ListEntries = new HashSet<ListEntry>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        [Column("Person_Id")]
        public Guid? PersonId { get; set; }
        [Column("Song_Id")]
        public Guid? SongId { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("Journals")]
        public virtual Person Person { get; set; }
        [ForeignKey("SongId")]
        [InverseProperty("Journals")]
        public virtual Song Song { get; set; }
        [InverseProperty("Journal")]
        public virtual ICollection<ListEntry> ListEntries { get; set; }
    }
}