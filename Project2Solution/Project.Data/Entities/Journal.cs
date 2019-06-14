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
            ListEntries = new List<ListEntry>();
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public int SongId { get; set; }
        [Required]
        public string Title { get; set; }

        public Person Person { get; set; }
        public Song Song { get; set; }
        public List<ListEntry> ListEntries { get; set; }
    }
}