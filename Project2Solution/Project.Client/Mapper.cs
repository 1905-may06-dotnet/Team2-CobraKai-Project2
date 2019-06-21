using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Lib = Project.Domain;

namespace Project.Client
{
    public static class Mapper
    {
        public static Project.Client.Entities.Person Map(Lib.Person person) => new Project.Client.Entities.Person
        {
            Id = person.Id,
            Email = person.Email,
            Username = person.Username,
            Password = person.Password,
            Firstname = person.FirstName,
            Lastname = person.LastName
        };

        public static Lib.Person Map(Project.Client.Entities.Person person) => new Lib.Person
        {
            Id = person.Id,
            Email = person.Email,
            Username = person.Username,
            Password = person.Password,
            FirstName = person.Firstname,
            LastName = person.Lastname
        };

        public static Project.Client.Entities.Song Map(Lib.Song song) => new Project.Client.Entities.Song
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

        public static Lib.Song Map(Project.Client.Entities.Song song) => new Lib.Song
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

        public static Project.Client.Entities.Playlist Map(Lib.PlayList pl) => new Project.Client.Entities.Playlist
        {
            Id = pl.Id,
            PersonId = pl.PersonId,
            SongId = pl.SongId,
            Title = pl.Title
        };

        public static Lib.PlayList Map(Project.Client.Entities.Playlist pl) => new Lib.PlayList
        {
            Id = pl.Id,
            PersonId = pl.PersonId,
            SongId = pl.SongId,
            Title = pl.Title
        };

        public static IEnumerable<Lib.Person> Map(IEnumerable<Project.Client.Entities.Person> persons) => persons.Select(Map);
        public static IEnumerable<Project.Client.Entities.Person> Map(IEnumerable<Lib.Person> persons) => persons.Select(Map);

        public static IEnumerable<Lib.Song> Map(IEnumerable<Project.Client.Entities.Song> songs) => songs.Select(Map);
        public static IEnumerable<Project.Client.Entities.Song> Map(IEnumerable<Lib.Song> songs) => songs.Select(Map);

        public static IEnumerable<Lib.PlayList> Map(IEnumerable<Project.Client.Entities.Playlist> plists) => plists.Select(Map);
        public static IEnumerable<Project.Client.Entities.Playlist> Map(IEnumerable<Lib.PlayList> plists) => plists.Select(Map);
    }
}