using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project.Data
{
    public partial class CobraKaiDbContext : DbContext
    {
        public CobraKaiDbContext()
        {
        }

        public CobraKaiDbContext(DbContextOptions<CobraKaiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Entities.Journal> Journals { get; set; }
        public virtual DbSet<Entities.ListEntry> ListEntries { get; set; }
        public virtual DbSet<Entities.MusicList> MusicLists { get; set; }
        public virtual DbSet<Entities.Person> People { get; set; }
        public virtual DbSet<Entities.Playlist> Playlists { get; set; }
        public virtual DbSet<Entities.Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Data Source=SILVERWEAPON\\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Entities.Journal>(entity =>
            {
                entity.HasIndex(e => e.PersonId)
                    .HasName("IX_FK_JournalPerson");

                entity.HasIndex(e => e.SongId)
                    .HasName("IX_FK_JournalSong");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Journals)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_JournalPerson");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.Journals)
                    .HasForeignKey(d => d.SongId)
                    .HasConstraintName("FK_JournalSong");
            });

            modelBuilder.Entity<Entities.ListEntry>(entity =>
            {
                entity.HasIndex(e => e.JournalId)
                    .HasName("IX_FK_ListEntryJournal");

                entity.HasIndex(e => e.MusicListId)
                    .HasName("IX_FK_PlaylistListEntry");

                entity.HasIndex(e => e.SongId)
                    .HasName("IX_FK_ListEntrySong");

                entity.HasIndex(e => e.SonglistId)
                    .HasName("IX_FK_ListEntrySonglist");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Journal)
                    .WithMany(p => p.ListEntries)
                    .HasForeignKey(d => d.JournalId)
                    .HasConstraintName("FK_ListEntryJournal");

                entity.HasOne(d => d.MusicList)
                    .WithMany(p => p.ListEntries)
                    .HasForeignKey(d => d.MusicListId)
                    .HasConstraintName("FK_PlaylistListEntry");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.ListEntries)
                    .HasForeignKey(d => d.SongId)
                    .HasConstraintName("FK_ListEntrySong");

                entity.HasOne(d => d.Songlist)
                    .WithMany(p => p.ListEntries)
                    .HasForeignKey(d => d.SonglistId)
                    .HasConstraintName("FK_ListEntrySonglist");
            });

            modelBuilder.Entity<Entities.MusicList>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Entities.Person>(entity =>
            {
                entity.HasIndex(e => e.MusicListId)
                    .HasName("IX_FK_MusicListPerson");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.MusicList)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.MusicListId)
                    .HasConstraintName("FK_MusicListPerson");
            });

            modelBuilder.Entity<Entities.Playlist>(entity =>
            {
                entity.HasIndex(e => e.PersonId)
                    .HasName("IX_FK_PersonPlaylist");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Playlists)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PersonPlaylist");
            });

            modelBuilder.Entity<Entities.Song>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}