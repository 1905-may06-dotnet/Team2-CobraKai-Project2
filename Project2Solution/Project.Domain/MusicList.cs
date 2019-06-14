using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain
{
    public class MusicList
    {
        public MusicList()
        {
            ListEntries = new List<ListEntry>();
            People = new List<Person>();
        }
        public int Id { get; set; }

        public int PersonId { get; set; }

        public List<ListEntry> ListEntries { get; set; }
        public List<Person> People { get; set; }
    }
}
