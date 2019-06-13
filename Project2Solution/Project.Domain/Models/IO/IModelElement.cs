using System;
using System.Runtime.CompilerServices; 

namespace Project.Domain.Models.IO {

    public abstract class IModelElement < T > : IModel < T >
        where T: new () {

        public static IModelElement < T > Empty = null;

	    public abstract IModelElement < T > Bind ( ref IModelElement < T > element );

        //public IModelElement ( IModelElement < T > element ) { _resource = element._resource; }

    }

}
