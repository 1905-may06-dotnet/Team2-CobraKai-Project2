using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project.Domain.Models.Elements {

    public class QueryMusicList : EventArgs {

        public Guid Id      = Guid.Empty;
        public Guid PersonId = Guid.Empty;

    }

    public class MusicList : IO.IWrite < Data.Entities.MusicList > {

        public MusicList () { _resource = new Data.Entities.MusicList (); }

        public MusicList ( MusicList musicList ) { _resource = musicList._resource; }

        public MusicList ( Data.Entities.MusicList musicList ) { _resource = musicList; }

        public override IO.IModelElement < Data.Entities.MusicList > Bind ( ref IO.IModelElement < Data.Entities.MusicList > element ) {

            _resource.PersonId    = element.Record.PersonId;
            _resource.ListEntries = element.Record.ListEntries;

            return this;

        }

        public IO.IModelElement < Data.Entities.MusicList > Bind ( ref IO.IModelElement < Data.Entities.Person > element ) {

            _resource.PersonId = element.Record.Id;

            return this;

        }

        public IO.IModelElement < Data.Entities.MusicList > Bind ( ref IO.IModelElement < Data.Entities.Song > element ) {

            _resource.ListEntries.Add ( new Project.Data.Entities.ListEntry { Song = element.Record, SongId = element.Record.Id } );

            return this;

        }

        public override IO.IWrite < Data.Entities.MusicList > Save () {

            lock ( _lock ) {

                using ( var context = new Project.Data.CobraKaiDbContext () ) {

                    var local = context.MusicLists.Find ( _resource.Id );

                    if ( local == null ) {

                        context.Attach ( _resource );
                        context.Add    ( _resource );

                    } else {

                        _resource.PersonId    = local.PersonId;
                        _resource.ListEntries = local.ListEntries;

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
