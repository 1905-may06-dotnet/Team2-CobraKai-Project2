using System;
using System.Collections.Generic;

namespace Project.Domain
{
    public class Journal
    {
        public Journal()
        {
            ListEntries = new List<ListEntry>();
        }
        public int Id { get; set; }

        public int PersonId { get; set; }
        public int SongId { get; set; }
        public string Title { get; set; }

        public Person Person { get; set; }
        public Song Song { get; set; }
        public List<ListEntry> ListEntries { get; set; }
    }
}
