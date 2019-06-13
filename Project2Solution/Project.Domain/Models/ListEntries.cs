using System;
using System.Collections.Generic;
using System.Text;
using Project.Domain.Models.Elements;
using System.Linq;

namespace Project.Domain.Models {

    public class ListEntries : IModels < Elements.ListEntry, Elements.QueryListEntry > {

        //
        protected override ListEntry Read ( QueryListEntry entityArgs ) {

            using ( var context = new Project.Data.CobraKaiDbContext () ) {

                if ( entityArgs.Id != null && entityArgs.Id != Guid.Empty )
                    return new Elements.ListEntry ( context.ListEntries.Find ( entityArgs.Id ) );
                else if ( entityArgs.SongId != null && entityArgs.SongId != Guid.Empty )
                    return new Elements.ListEntry ( context.ListEntries.Find ( entityArgs.SongId ) );
                else if ( entityArgs.MusiclistId != null && entityArgs.MusiclistId != Guid.Empty )
                    return new Elements.ListEntry ( context.ListEntries.Find ( entityArgs.MusiclistId ) );
                else return null;

            }

        }

        public override ListEntry Query ( ref Elements.QueryListEntry Index ) { return Read ( Index ); }

        protected override HashSet < Elements.ListEntry > ReadAll () {

            using ( var context = new Data.CobraKaiDbContext () ) { 

                var result = new HashSet < Elements.ListEntry > ();

                foreach ( var record in context.ListEntries )
                    result.Add ( new Elements.ListEntry ( record ) );

                return result;

            }

        }

    }

}