using System;
using System.Collections.Generic;
using System.Text;
using Project.Data.Entities;
using Project.Domain.Models.IO;

namespace Project.Domain.Models.Elements {

    public class QuerySong : EventArgs {

        public Guid Id = Guid.Empty;

        public string Title  = string.Empty;
        public string Artist = string.Empty;

        public QuerySong () {}

        public QuerySong ( Guid Id ) { this.Id = Id; }

        public QuerySong ( string artist, string title ) { Artist = artist; Title = title; }

    }

    public class Song : IO.IWrite < Data.Entities.Song > {

        public Song () { _resource = new Data.Entities.Song (); }

        public Song ( Song song ) { _resource = song._resource; }

        public Song ( Data.Entities.Song song ) { _resource = song; }

        public override IModelElement < Data.Entities.Song > Bind ( ref IModelElement < Data.Entities.Song > element ) {

            _resource.Artist   = element.Record.Artist;
            _resource.FilePath = element.Record.FilePath;

            return this;

        }

        public IModelElement < Data.Entities.Song > Bind ( ref IModelElement < Data.Entities.Journal > element ) {

            _resource.ListEntries = element.Record.ListEntries;
            _resource.Journals.Add ( element.Record );

            return this;

        }

        public override IO.IWrite < Data.Entities.Song > Save () {

            lock ( _lock ) {

                using ( var context = new Project.Data.CobraKaiDbContext () ) {

                    var local = context.Songs.Find ( _resource.Id );

                    if ( local == null ) {

                        context.Attach ( _resource );
                        context.Add    ( _resource );

                    } else {

                        _resource.Artist      = local.Artist;
                        _resource.FilePath    = local.FilePath;
                        _resource.Genre       = local.Genre;
                        _resource.Journals    = local.Journals;
                        _resource.ListEntries = local.ListEntries;
                        _resource.ReleaseDate = local.ReleaseDate;
                        _resource.Size        = local.Size;
                        _resource.Title       = local.Title;

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

    }

}
