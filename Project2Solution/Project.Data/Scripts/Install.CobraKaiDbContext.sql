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
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'People'
CREATE TABLE [CobraKai].[People] (
    [Id] uniqueidentifier  NOT NULL,
    [MusicListId] uniqueidentifier  NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Firstname] nvarchar(max)  NOT NULL,
    [Lastname] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [MusicList_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'Songs'
CREATE TABLE [CobraKai].[Songs] (
    [Id] uniqueidentifier  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Artist] nvarchar(max)  NOT NULL,
    [Genre] nvarchar(max)  NOT NULL,
    [Length] datetime  NOT NULL,
    [ReleasedD] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Playlists'
CREATE TABLE [CobraKai].[Playlists] (
    [Id] uniqueidentifier  NOT NULL,
    [PersonId] uniqueidentifier  NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MusicLists'
CREATE TABLE [CobraKai].[MusicLists] (
    [Id] uniqueidentifier  NOT NULL,
    [PersonId] uniqueidentifier  NULL
);
GO

-- Creating table 'Journals'
CREATE TABLE [CobraKai].[Journals] (
    [Id] uniqueidentifier  NOT NULL,
    [PersonId] uniqueidentifier  NULL,
    [SongId] uniqueidentifier  NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Person_Id] uniqueidentifier  NULL,
    [Song_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'ListEntries'
CREATE TABLE [CobraKai].[ListEntries] (
    [Id] uniqueidentifier  NOT NULL,
    [PlayListId] uniqueidentifier  NULL,
    [MusicListId] uniqueidentifier  NULL,
    [Favorite] nvarchar(max)  NOT NULL,
    [TimeStamp] datetime  NULL,
    [JournalEntry] nvarchar(max)  NOT NULL,
    [Songlist_Id] uniqueidentifier  NULL,
    [Song_Id] uniqueidentifier  NULL,
    [Journal_Id] uniqueidentifier  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [CobraKai].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Songs'
ALTER TABLE [CobraKai].[Songs]
ADD CONSTRAINT [PK_Songs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Playlists'
ALTER TABLE [CobraKai].[Playlists]
ADD CONSTRAINT [PK_Playlists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MusicLists'
ALTER TABLE [CobraKai].[MusicLists]
ADD CONSTRAINT [PK_MusicLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Journals'
ALTER TABLE [CobraKai].[Journals]
ADD CONSTRAINT [PK_Journals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ListEntries'
ALTER TABLE [CobraKai].[ListEntries]
ADD CONSTRAINT [PK_ListEntries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Songlist_Id] in table 'ListEntries'
ALTER TABLE [CobraKai].[ListEntries]
ADD CONSTRAINT [FK_ListEntrySonglist]
    FOREIGN KEY ([Songlist_Id])
    REFERENCES [CobraKai].[MusicLists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ListEntrySonglist'
CREATE INDEX [IX_FK_ListEntrySonglist]
ON [CobraKai].[ListEntries]
    ([Songlist_Id]);
GO

-- Creating foreign key on [Song_Id] in table 'ListEntries'
ALTER TABLE [CobraKai].[ListEntries]
ADD CONSTRAINT [FK_ListEntrySong]
    FOREIGN KEY ([Song_Id])
    REFERENCES [CobraKai].[Songs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ListEntrySong'
CREATE INDEX [IX_FK_ListEntrySong]
ON [CobraKai].[ListEntries]
    ([Song_Id]);
GO

-- Creating foreign key on [MusicList_Id] in table 'People'
ALTER TABLE [CobraKai].[People]
ADD CONSTRAINT [FK_MusicListPerson]
    FOREIGN KEY ([MusicList_Id])
    REFERENCES [CobraKai].[MusicLists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MusicListPerson'
CREATE INDEX [IX_FK_MusicListPerson]
ON [CobraKai].[People]
    ([MusicList_Id]);
GO

-- Creating foreign key on [PersonId] in table 'Playlists'
ALTER TABLE [CobraKai].[Playlists]
ADD CONSTRAINT [FK_PersonPlaylist]
    FOREIGN KEY ([PersonId])
    REFERENCES [CobraKai].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPlaylist'
CREATE INDEX [IX_FK_PersonPlaylist]
ON [CobraKai].[Playlists]
    ([PersonId]);
GO

-- Creating foreign key on [Person_Id] in table 'Journals'
ALTER TABLE [CobraKai].[Journals]
ADD CONSTRAINT [FK_JournalPerson]
    FOREIGN KEY ([Person_Id])
    REFERENCES [CobraKai].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JournalPerson'
CREATE INDEX [IX_FK_JournalPerson]
ON [CobraKai].[Journals]
    ([Person_Id]);
GO

-- Creating foreign key on [Song_Id] in table 'Journals'
ALTER TABLE [CobraKai].[Journals]
ADD CONSTRAINT [FK_JournalSong]
    FOREIGN KEY ([Song_Id])
    REFERENCES [CobraKai].[Songs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JournalSong'
CREATE INDEX [IX_FK_JournalSong]
ON [CobraKai].[Journals]
    ([Song_Id]);
GO

-- Creating foreign key on [MusicListId] in table 'ListEntries'
ALTER TABLE [CobraKai].[ListEntries]
ADD CONSTRAINT [FK_PlaylistListEntry]
    FOREIGN KEY ([MusicListId])
    REFERENCES [CobraKai].[Playlists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlaylistListEntry'
CREATE INDEX [IX_FK_PlaylistListEntry]
ON [CobraKai].[ListEntries]
    ([MusicListId]);
GO

-- Creating foreign key on [Journal_Id] in table 'ListEntries'
ALTER TABLE [CobraKai].[ListEntries]
ADD CONSTRAINT [FK_ListEntryJournal]
    FOREIGN KEY ([Journal_Id])
    REFERENCES [CobraKai].[Journals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ListEntryJournal'
CREATE INDEX [IX_FK_ListEntryJournal]
ON [CobraKai].[ListEntries]
    ([Journal_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------