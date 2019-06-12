using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Entities
{
    [Table("MusicLists", Schema = "CobraKai")]
    public partial class MusicList
    {
        public MusicList()
        {
            ListEntries = new HashSet<ListEntry>();
            People = new HashSet<Person>();
        }

        public Guid Id { get; set; }
        public Guid? PersonId { get; set; }

        [InverseProperty("Songlist")]
        public virtual ICollection<ListEntry> ListEntries { get; set; }
        [InverseProperty("MusicListId1Navigation")]
        public virtual ICollection<Person> People { get; set; }
    }
}