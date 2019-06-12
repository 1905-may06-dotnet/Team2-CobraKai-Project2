using System;
using System.Collections.Generic;
using System.Text;
using Project.Domain.Models.Elements;

namespace Project.Domain.Models {

    public class ListEntries : IModels < Elements.ListEntry, Elements.QueryListEntry > {

        protected override ListEntry Read ( QueryListEntry entityArgs ) {

            using ( var context = new Project.Data.CobraKaiDbContext () ) {

                if ( entityArgs.Id != null && entityArgs.Id != Guid.Empty ) {

                    return new Elements.ListEntry ( context.ListEntries.Find ( entityArgs.Id ) );

                } else if ( entityArgs.JournalId != null && entityArgs.JournalId != Guid.Empty ) { 

                    return new Elements.ListEntry ( context.ListEntries.Find ( entityArgs.JournalId ) );

                } else if ( entityArgs.MusicListId != null && entityArgs.MusicListId != Guid.Empty ) { 

                    return new Elements.ListEntry ( context.ListEntries.Find ( entityArgs.MusicListId ) );

                } else if ( entityArgs.PlayListId != null && entityArgs.PlayListId != Guid.Empty ) { 

                    return new Elements.ListEntry ( context.ListEntries.Find ( entityArgs.PlayListId ) );

                } else if ( entityArgs.SongId != null && entityArgs.SongId != Guid.Empty ) { 

                    return new Elements.ListEntry ( context.ListEntries.Find ( entityArgs.SongId ) );

                } else if ( entityArgs.SonglistId != null && entityArgs.SonglistId != Guid.Empty ) {

                    return new Elements.ListEntry ( context.ListEntries.Find ( entityArgs.SonglistId ) );

                } else return null;

            }

        }

        public override ListEntry Query ( ref QueryListEntry Index ) { return Read ( Index ); }

        protected override HashSet < ListEntry > ReadAll () {

            using ( var context = new Data.CobraKaiDbContext () ) { 

                var result = new HashSet < Elements.ListEntry > ();

                foreach ( var record in context.ListEntries ) 
                    result.Add ( new Elements.ListEntry ( record ) );

                return result;

            }

        }

    }

}
