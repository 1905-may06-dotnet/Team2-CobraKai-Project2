using System;

using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata;

namespace Project.Data.Entities

{

    public class CobraKaiDbContext : DbContext

    {
        public CobraKaiDbContext()
        {
        }

        public virtual DbSet<Entities.Journal> Journals { get; set; }

        public virtual DbSet<Entities.Person> People { get; set; }

        public virtual DbSet<Entities.Playlist> Playlists { get; set; }

        public virtual DbSet<Entities.Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {

            if (!optionsBuilder.IsConfigured)

                optionsBuilder.UseSqlServer("Server=tcp:utadbserverdc.database.windows.net,1433;Initial Catalog=Project2DB;Persist Security Info=False;User ID=danielcoombs005;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Journal>(entity =>
            {
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Journal)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PersonId_ToJournal");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.Journals)
                    .HasForeignKey(d => d.SongId)
                    .HasConstraintName("FK_SongId_ToJournal");
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Playlist)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PersonId_ToPlaylist");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.Playlists)
                    .HasForeignKey(d => d.SongId)
                    .HasConstraintName("FK_SongId_ToPlaylist");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.Property(e => e.Size).HasColumnType("decimal(18, 0)");
            });
        }
    }

}

