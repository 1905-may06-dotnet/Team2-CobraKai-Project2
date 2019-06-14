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
            ListEntries = new List<ListEntry>();
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Title { get; set; }

        public Person Person { get; set; }
        public List<ListEntry> ListEntries { get; set; }
    }
}