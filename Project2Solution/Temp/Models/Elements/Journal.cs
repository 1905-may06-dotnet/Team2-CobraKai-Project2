using System;
using System.Collections.Generic;
using System.Text;
using Project.Data.Entities;
using Project.Domain.Models.IO;

namespace Project.Domain.Models.Elements {

    public class QueryJournal : EventArgs {

        public Guid   Id          = Guid.Empty;
        public string JournalName = string.Empty; //<Prefix with person username

        //Potentially special collection applications
        public Guid? PersonId     = Guid.Empty;
        public Guid? SongId       = Guid.Empty;

        public QueryJournal () {}

        public QueryJournal ( Guid Id ) { this.Id = Id; }

        public QueryJournal ( Guid? songId, string journalName = null ) { this.SongId = songId; JournalName = journalName; }

        public QueryJournal ( Guid? personId, Guid? songId ) { PersonId = personId; SongId = songId; }

    }

    public class Journal : IO.IWrite < Data.Entities.Journal > {

        public Journal () { _resource = new Data.Entities.Journal (); }

        public Journal ( Models.Elements.Journal record ) { _resource = record._resource; }

        public Journal ( Data.Entities.Journal record ) { _resource = record; }

        public override IModelElement < Data.Entities.Journal > Bind ( ref IModelElement < Data.Entities.Journal > element ) {

            _resource.PersonId = element.Record.PersonId;
            _resource.Person   = element.Record.Person;
            _resource.Title    = element.Record.Title;

            foreach ( var record in element.Record.ListEntries )
                _resource.ListEntries.Add ( record );

            return this;

        }

        public IModelElement < Data.Entities.Journal > Bind ( ref IModelElement < Data.Entities.Person > element ) {

            _resource.PersonId = element.Record.Id;
            _resource.Person   = element.Record;

            return this;

        }

        public IModelElement < Data.Entities.Journal > Bind ( ref IModelElement < Data.Entities.Song > element ) {

            _resource.SongId = element.Id;
            _resource.Song   = element.Record; 

            return this;

        }

        public override IWrite < Data.Entities.Journal > Save () {

            lock ( _lock ) {

                using ( var context = new Project.Data.CobraKaiDbContext () ) {

                    var local = context.Journals.Find ( _resource.Id );

                    if ( local == null ) {

                        context.Attach ( _resource );
                        context.Add    ( _resource );

                    } else {

                        local.Title       = _resource.Title;
                        local.ListEntries = _resource.ListEntries;

                        local.PersonId    = _resource.PersonId;
                        local.Person      = _resource.Person;
                        local.SongId      = _resource.SongId;
                        local.Song        = _resource.Song; 

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

    }

}
