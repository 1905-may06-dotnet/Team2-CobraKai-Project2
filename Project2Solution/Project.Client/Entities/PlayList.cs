namespace Project.Client.Entities
{
    public partial class Playlist
    {
        public int Id { get; set; }

        public int? PersonId { get; set; }
        public int? SongId { get; set; }
        public string Title { get; set; }

        public virtual Person Person { get; set; }
        public virtual Song Song { get; set; }
    }
}
