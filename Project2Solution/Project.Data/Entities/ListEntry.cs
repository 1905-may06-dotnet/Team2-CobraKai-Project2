using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Entities
{
    [Table("ListEntries", Schema = "CobraKai")]
    public partial class ListEntry
    {
        public Guid Id { get; set; }
        public Guid? PlayListId { get; set; }
        public Guid? MusicListId { get; set; }
        [Required]
        public string Favorite { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimeStamp { get; set; }
        [Required]
        public string JournalEntry { get; set; }
        [Column("Songlist_Id")]
        public Guid? SonglistId { get; set; }
        [Column("Song_Id")]
        public Guid? SongId { get; set; }
        [Column("Journal_Id")]
        public Guid? JournalId { get; set; }

        [ForeignKey("JournalId")]
        [InverseProperty("ListEntries")]
        public virtual Journal Journal { get; set; }
        [ForeignKey("MusicListId")]
        [InverseProperty("ListEntries")]
        public virtual Playlist MusicList { get; set; }
        [ForeignKey("SongId")]
        [InverseProperty("ListEntries")]
        public virtual Song Song { get; set; }
        [ForeignKey("SonglistId")]
        [InverseProperty("ListEntries")]
        public virtual MusicList Songlist { get; set; }
    }
}