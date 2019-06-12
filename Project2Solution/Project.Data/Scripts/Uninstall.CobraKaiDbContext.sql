SET QUOTED_IDENTIFIER OFF;
GO
USE [tempdb];
GO
IF SCHEMA_ID(N'CobraKai') IS NULL EXECUTE(N'CREATE SCHEMA [CobraKai]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[CobraKai].[FK_ListEntrySonglist]', 'F') IS NOT NULL
    ALTER TABLE [CobraKai].[ListEntries] DROP CONSTRAINT [FK_ListEntrySonglist];
GO
IF OBJECT_ID(N'[CobraKai].[FK_ListEntrySong]', 'F') IS NOT NULL
    ALTER TABLE [CobraKai].[ListEntries] DROP CONSTRAINT [FK_ListEntrySong];
GO
IF OBJECT_ID(N'[CobraKai].[FK_MusicListPerson]', 'F') IS NOT NULL
    ALTER TABLE [CobraKai].[People] DROP CONSTRAINT [FK_MusicListPerson];
GO
IF OBJECT_ID(N'[CobraKai].[FK_PersonPlaylist]', 'F') IS NOT NULL
    ALTER TABLE [CobraKai].[Playlists] DROP CONSTRAINT [FK_PersonPlaylist];
GO
IF OBJECT_ID(N'[CobraKai].[FK_JournalPerson]', 'F') IS NOT NULL
    ALTER TABLE [CobraKai].[Journals] DROP CONSTRAINT [FK_JournalPerson];
GO
IF OBJECT_ID(N'[CobraKai].[FK_JournalSong]', 'F') IS NOT NULL
    ALTER TABLE [CobraKai].[Journals] DROP CONSTRAINT [FK_JournalSong];
GO
IF OBJECT_ID(N'[CobraKai].[FK_PlaylistListEntry]', 'F') IS NOT NULL
    ALTER TABLE [CobraKai].[ListEntries] DROP CONSTRAINT [FK_PlaylistListEntry];
GO
IF OBJECT_ID(N'[CobraKai].[FK_ListEntryJournal]', 'F') IS NOT NULL
    ALTER TABLE [CobraKai].[ListEntries] DROP CONSTRAINT [FK_ListEntryJournal];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[CobraKai].[People]', 'U') IS NOT NULL
    DROP TABLE [CobraKai].[People];
GO
IF OBJECT_ID(N'[CobraKai].[Songs]', 'U') IS NOT NULL
    DROP TABLE [CobraKai].[Songs];
GO
IF OBJECT_ID(N'[CobraKai].[Playlists]', 'U') IS NOT NULL
    DROP TABLE [CobraKai].[Playlists];
GO
IF OBJECT_ID(N'[CobraKai].[MusicLists]', 'U') IS NOT NULL
    DROP TABLE [CobraKai].[MusicLists];
GO
IF OBJECT_ID(N'[CobraKai].[Journals]', 'U') IS NOT NULL
    DROP TABLE [CobraKai].[Journals];
GO
IF OBJECT_ID(N'[CobraKai].[ListEntries]', 'U') IS NOT NULL
    DROP TABLE [CobraKai].[ListEntries];
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------