
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain
{
    public class Song
    {
        public Song()
        {
            Journals = new List<Journal>();
            ListEntries = new List<ListEntry>();
        }

        public int Id { get; set; }

        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public decimal Size {get;set;}
        public string Length { get; set; }
        public string ReleaseDate { get; set; }
        public string FilePath { get; set; }

        public List<Journal> Journals { get; set; }
        public List<ListEntry> ListEntries { get; set; }
    }
}
