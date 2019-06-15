using System;
using System.Collections.Generic;
using System.Text;
using Project.Data.Entities;
using System.Linq;

namespace Project.Data
{
    public static class Mapper
    {
        public static Domain.Journal Map(Journal journal) => new Domain.Journal
        {
            Id = journal.Id,
            PersonId = journal.PersonId,
            SongId = journal.SongId,
            Title = journal.Title,
            JournalEntry = journal.JournalEntry
        };

        public static Journal Map(Domain.Journal journal) => new Journal
        {
            Id = journal.Id,
            PersonId = journal.PersonId,
            SongId = journal.SongId,
            Title = journal.Title,
            JournalEntry = journal.JournalEntry
        };

        public static Domain.Song Map(Song song) => new Domain.Song
        {
            Id = song.Id,
            Title = song.Title,
            Artist = song.Artist,
            Genre = song.Genre,
            Size = song.Size,
            Length = song.Length,
            ReleaseDate = song.ReleaseDate,
            FilePath = song.FilePath
        };

        public static Song Map(Domain.Song song) => new Song
        {
            Id = song.Id,
            Title = song.Title,
            Artist = song.Artist,
            Genre = song.Genre,
            Size = song.Size,
            Length = song.Length,
            ReleaseDate = song.ReleaseDate,
            FilePath = song.FilePath

        };

        public static Person Map(Domain.Person person) => new Person
        {
            Id = person.Id,
            Email = person.Email,
            Username = person.Username,
            Password = person.Password,
            Firstname = person.FirstName,
            Lastname = person.LastName
        };

        public static Domain.Person Map(Person person) => new Domain.Person
        {
            Id = person.Id,
            Email = person.Email,
            Username = person.Username,
            Password = person.Password,
            FirstName = person.Firstname,
            LastName = person.Lastname
        };

        public static Domain.PlayList Map(Playlist playlist) => new Domain.PlayList
        {
            Id = playlist.Id,
            PersonId = playlist.PersonId,
            SongId = playlist.SongId,
            Title = playlist.Title
        };

        public static Playlist Map(Domain.PlayList playlist) => new Playlist
        {
            Id = playlist.Id,
            PersonId = playlist.PersonId,
            SongId = playlist.SongId,
            Title = playlist.Title
        };



        public static IEnumerable<Domain.Journal> Map(IEnumerable<Journal> journals) => journals.Select(Map);
        public static IEnumerable<Journal> Map(IEnumerable<Domain.Journal> journals) => journals.Select(Map);

        public static IEnumerable<Domain.Person> Map(IEnumerable<Person> persons) => persons.Select(Map);
        public static IEnumerable<Person> Map(IEnumerable<Domain.Person> persons) => persons.Select(Map);

        public static IEnumerable<Domain.PlayList> Map(IEnumerable<Playlist> plist) => plist.Select(Map);
        public static IEnumerable<Playlist> Map(IEnumerable<Domain.PlayList> plist) => plist.Select(Map);

        public static IEnumerable<Domain.Song> Map(IEnumerable<Song> songs) => songs.Select(Map);
        public static IEnumerable<Song> Map(IEnumerable<Domain.Song> songs) => songs.Select(Map);
        
    }
}
