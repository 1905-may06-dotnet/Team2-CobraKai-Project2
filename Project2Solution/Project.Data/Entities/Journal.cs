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
        public Guid? PersonId { get; set; }
        public Guid? SongId { get; set; }
        [Required]
        public string Title { get; set; }
        [Column("Person_Id")]
        public Guid? PersonId1 { get; set; }
        [Column("Song_Id")]
        public Guid? SongId1 { get; set; }

        [ForeignKey("PersonId1")]
        [InverseProperty("Journals")]
        public virtual Person PersonId1Navigation { get; set; }
        [ForeignKey("SongId1")]
        [InverseProperty("Journals")]
        public virtual Song SongId1Navigation { get; set; }
        [InverseProperty("Journal")]
        public virtual ICollection<ListEntry> ListEntries { get; set; }
    }
}