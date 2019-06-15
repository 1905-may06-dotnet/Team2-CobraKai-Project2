using System;
using System.Collections.Generic;
using System.Text;
using Project.Data.Entities;
using System.Linq;

namespace Project.Data
{
    public static class Mapper
    {/*
        public static Domain.Journal Map(Journal journal) => new Domain.Journal
        {
            Id = journal.Id,
            PersonId = journal.PersonId,
            SongId = journal.SongId,
            Title = journal.Title
        };

        public static Journal Map(Domain.Journal journal) => new Journal
        {
            Id = journal.Id,
            PersonId = journal.PersonId,
            SongId = journal.SongId,
            Title = journal.Title
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
            Lastname = person.LastName,
            MusicListId = person.MusicListId
        };

        public static Domain.Person Map(Person person) => new Domain.Person
        {
            Id = person.Id,
            Email = person.Email,
            Username = person.Username,
            Password = person.Password,
            FirstName = person.Firstname,
            LastName = person.Lastname,
            MusicListId = person.MusicListId
        };

        public static Domain.PlayList Map(Playlist playlist) => new Domain.PlayList
        {
            Id = playlist.Id,
            PersonId = playlist.PersonId,
            Title = playlist.Title

        };

        public static Playlist Map(Domain.PlayList playlist) => new Playlist
        {
            Id = playlist.Id,
            PersonId = playlist.PersonId,
            Title = playlist.Title
        };

        public static Domain.MusicList Map(MusicList musicList) => new Domain.MusicList
        {
            Id = musicList.Id,
            PersonId = musicList.PersonId,
        };

        public static MusicList Map(Domain.MusicList musicList) => new MusicList
        {
            Id = musicList.Id,
            PersonId = musicList.PersonId,
        };

        public static Domain.ListEntry Map(ListEntry listEntry) => new Domain.ListEntry
        {
            Id = listEntry.Id,
            Favorite = listEntry.Favorite,
            TimeStamp = listEntry.TimeStamp,
            SongId = listEntry.SongId,
            SongListId = listEntry.SonglistId,
            PlayListId = listEntry.PlayListId,
            MusicListId = listEntry.MusicListId,
            JournalId = listEntry.JournalId
        };

        public static ListEntry Map(Domain.ListEntry listEntry) => new ListEntry
        {
            Id = listEntry.Id,
            Favorite = listEntry.Favorite,
            TimeStamp = listEntry.TimeStamp,
            SongId = listEntry.SongId,
            SonglistId = listEntry.SongListId,
            PlayListId = listEntry.PlayListId,
            MusicListId = listEntry.MusicListId,
            JournalId = listEntry.JournalId
        };

        public static IEnumerable<Domain.Journal> Map(IEnumerable<Journal> journals) => journals.Select(Map);
        public static IEnumerable<Journal> Map(IEnumerable<Domain.Journal> journals) => journals.Select(Map);

        public static IEnumerable<Domain.ListEntry> Map(IEnumerable<ListEntry> entries) => entries.Select(Map);
        public static IEnumerable<ListEntry> Map(IEnumerable<Domain.ListEntry> entries) => entries.Select(Map);

        public static IEnumerable<Domain.MusicList> Map(IEnumerable<MusicList> mlist) => mlist.Select(Map);
        public static IEnumerable<MusicList> Map(IEnumerable<Domain.MusicList> mlist) => mlist.Select(Map);

        public static IEnumerable<Domain.Person> Map(IEnumerable<Person> persons) => persons.Select(Map);
        public static IEnumerable<Person> Map(IEnumerable<Domain.Person> persons) => persons.Select(Map);

        public static IEnumerable<Domain.PlayList> Map(IEnumerable<Playlist> plist) => plist.Select(Map);
        public static IEnumerable<Playlist> Map(IEnumerable<Domain.PlayList> plist) => plist.Select(Map);

        public static IEnumerable<Domain.Song> Map(IEnumerable<Song> songs) => songs.Select(Map);
        public static IEnumerable<Song> Map(IEnumerable<Domain.Song> songs) => songs.Select(Map);
        */
    }
}
