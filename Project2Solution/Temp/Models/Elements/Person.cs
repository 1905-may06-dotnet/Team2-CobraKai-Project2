using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models.Elements {

    public class QueryPerson : EventArgs {

        public Guid   Id        = Guid.Empty;
        public string Email     = string.Empty;
        public string Password  = string.Empty;
        public string Username  = string.Empty;

    }

    public class Person : IO.IWrite < Data.Entities.Person > {

        public Person () { _resource = new Data.Entities.Person (); }

        public Person ( Person person ) { _resource = person._resource; }

        public Person ( Data.Entities.Person person ) { _resource = person; }

        public override IO.IModelElement < Data.Entities.Person > Bind ( ref IO.IModelElement < Data.Entities.Person > element ) {

            _resource = element.Record;

            return this;

        }

        public IO.IModelElement < Data.Entities.Person > Bind ( ref IO.IModelElement < Data.Entities.Playlist > element ) {

            element.Record.Person = _resource;
            element.Record.PersonId = _resource.Id;

            _resource.Playlists.Add ( element.Record );

            return this;

        }

        public IO.IModelElement < Data.Entities.Person > Bind ( ref IO.IModelElement < Data.Entities.MusicList > element ) {

            element.Record.PersonId = _resource.Id;

            _resource.MusicListId = element.Record.Id;
            _resource.MusicList   = element.Record;

            return this;

        }

        public IO.IModelElement < Data.Entities.Person > Bind ( ref IO.IModelElement < Data.Entities.Journal > element ) {

            element.Record.Person = _resource;
            element.Record.PersonId = _resource.Id;

            _resource.Journals.Add ( element.Record );

            return this;

        }

        public override IO.IWrite < Data.Entities.Person > Save () {

            lock ( _lock ) {

                using ( var context = new Project.Data.CobraKaiDbContext () ) {

                    var local = context.People.Find ( _resource.Id );

                    if ( local == null ) {

                        context.Attach ( _resource );
                        context.Add    ( _resource );

                    } else {

                        local.Email       = _resource.Email;
                        local.Firstname   = _resource.Firstname;
                        local.Journals    = _resource.Journals;
                        local.Lastname    = _resource.Lastname;
                        local.MusicList   = _resource.MusicList;
                        local.MusicListId = _resource.MusicListId;
                        local.Password    = _resource.Password;
                        local.Playlists   = _resource.Playlists;
                        local.Username    = _resource.Username;

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

        //add/remove to music list (+upload)
        //add/remove to playlist (associate)



    }

}
