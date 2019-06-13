using System;
using System.Runtime.CompilerServices;

namespace Project.Domain.Models.Elements {

    public class QueryListEntry : EventArgs {

        public Guid Id          = Guid.Empty;
        public Guid SongId      = Guid.Empty;

        //Special aggregate applications applications
        public Guid PlaylistId  = Guid.Empty;
        public Guid MusiclistId = Guid.Empty;

    }

    public class ListEntry : IO.IWrite < Data.Entities.ListEntry > {

        public ListEntry () {

            _resource              = new Data.Entities.ListEntry ();

            _resource.Id           = Guid.NewGuid ();
            _resource.Favorite     = false;
            _resource.JournalEntry = string.Empty;
            _resource.TimeStamp    = DateTime.Now;

        }

        public ListEntry ( Models.Elements.ListEntry record ) { _resource = record._resource; }

        public ListEntry ( Data.Entities.ListEntry record ) { _resource = record; }

        [ MethodImpl ( MethodImplOptions.AggressiveInlining ) ]
        public override IO.IModelElement < Data.Entities.ListEntry > Bind ( ref IO.IModelElement < Data.Entities.ListEntry > element ) {

            _resource.MusicListId = element.Record.MusicListId;
            _resource.MusicList   = element.Record.MusicList;
            _resource.JournalId   = element.Record.JournalId;
            _resource.PlayListId  = element.Record.PlayListId;
            _resource.SongId      = element.Record.SongId;
            _resource.Songlist    = element.Record.Songlist;
            _resource.SonglistId  = element.Record.SonglistId;

            return this;

        }

        [ MethodImpl ( MethodImplOptions.AggressiveInlining ) ]
        public IO.IModelElement < Data.Entities.ListEntry > Bind ( ref IO.IModelElement < Data.Entities.Journal > element ) {

            //JUMP from journal to person
            _resource.SongId     = element.Record.SongId;

            return this;

        }

        [ MethodImpl ( MethodImplOptions.AggressiveInlining ) ]
        public IO.IModelElement < Data.Entities.ListEntry > Bind ( ref IO.IModelElement < Data.Entities.MusicList > element ) {

            //JUMP from musiclist to person
            _resource.MusicListId = element.Id;

            return this;

        }

        [ MethodImpl ( MethodImplOptions.AggressiveInlining ) ]
        public IO.IModelElement < Data.Entities.ListEntry > Bind ( ref IO.IModelElement < Data.Entities.Playlist > element ) {

            //JUMP from playlist to person
            _resource.PlayListId = element.Id;

            return this;

        }

        [ MethodImpl ( MethodImplOptions.AggressiveInlining ) ]
        public IO.IModelElement < Data.Entities.ListEntry > Bind ( ref IO.IModelElement < Data.Entities.Song > element ) {

            _resource.SongId = element.Id;
            _resource.Song   = element.Record;
            
            return this;

        }

        public override IO.IWrite < Data.Entities.ListEntry > Save () {

            lock ( _lock ) {

                using ( var context = new Project.Data.CobraKaiDbContext () ) {

                    var local = context.ListEntries.Find ( _resource.Id );

                    if ( local == null ) {

                        context.Attach ( _resource );
                        context.Add    ( _resource );

                    } else {

                        local.Favorite     = _resource.Favorite;
                        local.TimeStamp    = _resource.TimeStamp;

                        local.Journal      = _resource.Journal;
                        local.JournalEntry = _resource.JournalEntry;
                        local.JournalId    = _resource.JournalId;

                        local.MusicList    = _resource.MusicList;
                        local.MusicListId  = _resource.MusicListId;

                        local.PlayListId   = _resource.PlayListId;

                        local.Song         = _resource.Song;
                        local.SongId       = _resource.SongId;

                        local.Songlist     = _resource.Songlist;
                        local.SonglistId   = _resource.SonglistId;

                        context.Update ( local );

                    }
                    
                    context.SaveChanges ();

                }

            }

            return this;

        }

        public override void Delete () {

            lock ( _lock ) {

                using ( var context = new Project.Data.CobraKaiDbContext () ) {

                    context.Remove ( _resource );
                    context.SaveChanges ();

                }

            }

        }

        public override Guid Id { get { return _resource.Id; } }

        public bool? Favorite { get { return _resource.Favorite; } set { _resource.Favorite = value; } }

        public string JournalEntry { get { return _resource.JournalEntry; } set { _resource.JournalEntry = value; } }

        public DateTime TimeStamp { get { return _resource.TimeStamp; } set { _resource.TimeStamp = value; } }

    }

}
