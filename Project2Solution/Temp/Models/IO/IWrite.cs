using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models.IO {

    public abstract class IWrite < T > : IO.IModelElement < T > 
        where T: new () {

        protected readonly object _lock = new object ();

        public abstract IWrite < T > Save ();

        public abstract void Delete ();

    }

}
