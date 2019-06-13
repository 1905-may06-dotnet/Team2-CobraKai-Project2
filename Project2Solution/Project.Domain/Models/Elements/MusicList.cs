using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models.Elements {

    public class QueryMusicList : EventArgs {

        

    }

    public class MusicList : IO.IWrite < Data.Entities.MusicList > {

        public MusicList () { _resource = new Data.Entities.MusicList (); }

        public MusicList ( MusicList musicList ) { _resource = musicList._resource; }

        public MusicList ( Data.Entities.MusicList musicList ) { _resource = musicList; }

        public override IO.IModelElement < Data.Entities.MusicList > Bind ( ref IO.IModelElement < Data.Entities.MusicList > element ) {

            throw new NotImplementedException ();

        }

        public override IO.IWrite < Data.Entities.MusicList > Save () {

            lock ( _lock ) {

                using ( var context = new Project.Data.CobraKaiDbContext () ) {

                    var local = context.MusicLists.Find ( _resource.Id );

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
