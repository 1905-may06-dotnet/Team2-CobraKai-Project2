using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models.Elements {

    public class QueryPlayList : EventArgs {

        Guid Id       = Guid.Empty;
        Guid PersonId = Guid.Empty;

    }

    public class PlayList : IO.IWrite < Data.Entities.Playlist > {

        public PlayList () { _resource = new Data.Entities.Playlist (); }

        public PlayList ( PlayList playList ) { _resource = playList._resource; }

        public PlayList ( Data.Entities.Playlist playlist ) { _resource = playlist; }

        public override IO.IModelElement < Data.Entities.Playlist > Bind ( ref IO.IModelElement < Data.Entities.Playlist > element ) {

            _resource.ListEntries = element.Record.ListEntries;
            _resource.Person      = element.Record.Person;
            _resource.PersonId    = element.Record.PersonId;

            return this;

        }

        public IO.IModelElement < Data.Entities.Playlist > Bind ( ref IO.IModelElement < Data.Entities.Person > element ) {

            _resource.Person      = element.Record;
            _resource.PersonId    = element.Record.Id;

            return this;

        }

        public override IO.IWrite < Data.Entities.Playlist > Save () {

            lock ( _lock ) {

                using ( var context = new Project.Data.CobraKaiDbContext () ) {

                    var local = context.Playlists.Find ( _resource.Id );

                    if ( local == null ) {

                        context.Attach ( _resource );
                        context.Add    ( _resource );

                    } else {

                        local.ListEntries = _resource.ListEntries;
                        local.Person      = _resource.Person;
                        local.PersonId    = _resource.PersonId;
                        local.Title       = _resource.Title;

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
