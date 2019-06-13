using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models.Elements {

    public class QueryPlayList : EventArgs {

        

    }

    public class PlayList : IO.IWrite < Data.Entities.Playlist > {

        public PlayList () { _resource = new Data.Entities.Playlist (); }

        public PlayList ( PlayList playList ) { _resource = playList._resource; }

        public PlayList ( Data.Entities.Playlist playlist ) { _resource = playlist; }

        public override IO.IModelElement < Data.Entities.Playlist > Bind ( ref IO.IModelElement < Data.Entities.Playlist > element ) {

            throw new NotImplementedException ();

        }

        public override IO.IWrite < Data.Entities.Playlist > Save () {

            lock ( _lock ) {

                using ( var context = new Project.Data.CobraKaiDbContext () ) {

                    var local = context.Playlists.Find ( _resource.Id );

                    if ( local == null ) {

                        context.Attach ( _resource );
                        context.Add    ( _resource );

                    } else {

                        //Variable assignment here

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
