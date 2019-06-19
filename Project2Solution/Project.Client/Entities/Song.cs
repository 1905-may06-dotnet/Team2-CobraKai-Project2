
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Client.Entities
{
    public class Song
    {
        //gets or sets song id
        //to be referenced in Journal and PlayList tables
        public int Id { get; set; }
        //gets or sets title of song
        public string Title { get; set; }
        //gets or sets artist of song
        public string Artist { get; set; }
        //gets or sets song's genre
        public string Genre { get; set; }
        //gets or sets size of song
        public decimal Size {get;set;}
        //gets or sets length of song (MM:SS)
        public string Length { get; set; }
        //gets or sets release date of song (YYYY)
        public string ReleaseDate { get; set; }
        //gets or sets path of file of song
        public string FilePath { get; set; }
    }
}
