using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models.Elements {

    public class QueryPerson : EventArgs {

        

    }

    public class Person : IO.IWrite < Data.Entities.Person > {

        public Person () { _resource = new Data.Entities.Person (); }

        public Person ( Person person ) { _resource = person._resource; }

        public Person ( Data.Entities.Person person ) { _resource = person; }

        public override IO.IModelElement < Data.Entities.Person > Bind ( ref IO.IModelElement < Data.Entities.Person > element ) {

            throw new NotImplementedException ();

        }

        public override IO.IWrite < Data.Entities.Person > Save () {

            lock ( _lock ) {

                using ( var context = new Project.Data.CobraKaiDbContext () ) {

                    var local = context.People.Find ( _resource.Id );

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

        //add/remove to music list (+upload)
        //add/remove to playlist (associate)



    }

}
