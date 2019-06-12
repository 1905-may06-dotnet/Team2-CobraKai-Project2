using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Project.Data;
using Project.Data.Entities;

namespace Project.Domain.Models.Elements {

    public class QueryListEntry : EventArgs {

        public Guid Id          = Guid.Empty;
        public Guid MusicListId = Guid.Empty;
        public Guid MusicList   = Guid.Empty;
        public Guid JournalId   = Guid.Empty;
        public Guid PlayListId  = Guid.Empty;
        public Guid SongId      = Guid.Empty;
        public Guid SonglistId  = Guid.Empty;

    }

    public class ListEntry : IO.IWrite < Data.Entities.ListEntry > {

        public ListEntry () {

            _resource = new Data.Entities.ListEntry ();

            _resource.Id           = Guid.NewGuid ();
            _resource.Favorite     = string.Empty;
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

        public override IO.IWrite < Data.Entities.ListEntry > Save () {

            lock ( _lock ) {

                using ( var context = new Project.Data.CobraKaiDbContext () ) {

                    var local = context.ListEntries.Find ( _resource.Id );

                    if ( local == null ) {

                        context.Attach ( _resource );
                        context.Add    ( _resource );

                    } else {

                        local.Favorite     = _resource.Favorite;
                        local.JournalEntry = _resource.JournalEntry;
                        local.TimeStamp    = _resource.TimeStamp;

                        context.Update ( local );

                    }
                    
                    context.SaveChanges ();

                }

            }

            return this;

        }

        public virtual void Delete () {

            lock ( _lock ) {

                using ( var context = new Project.Data.CobraKaiDbContext () ) {

                    context.Remove ( _resource );
                    context.SaveChanges ();

                }

            }

        }

        public override Guid Id { get { return _resource.Id; } }

        public string Favorite { get { return _resource.Favorite; } set { _resource.Favorite = value; } }

        public string JournalEntry { get { return _resource.JournalEntry; } set { _resource.JournalEntry = value; } }

        public DateTime? TimeStamp { get { return _resource.TimeStamp; } set { _resource.TimeStamp = value; } }

    }

}
