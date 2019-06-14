using System;
using System.Collections.Generic;

namespace Project.Domain.Models {

    public abstract class IModels < T, Ty > : IO.IRead < T, Ty >
        where T: new ()
        where Ty: EventArgs, new () {

        abstract public T Query ( ref Ty Index );

        public HashSet < T > Records { get { return ReadAll (); } }

    }

}