using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain
{
    public class PlayList
    {
        public PlayList()
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
