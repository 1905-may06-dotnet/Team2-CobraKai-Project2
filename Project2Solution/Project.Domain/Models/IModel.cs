using System;

namespace Project.Domain.Models {

    public abstract class IModel < T > {

        protected T _resource;

        public virtual Guid Id { get; }

        public virtual T Record { get { return _resource; } }

    }

}
