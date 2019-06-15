﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Entities
{
    public partial class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public decimal Size { get; set; }
        public string Length { get; set; }
        public string ReleaseDate { get; set; }
        public string FilePath { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; }
        public virtual ICollection<Journal> Journals { get; set; }
    }
}