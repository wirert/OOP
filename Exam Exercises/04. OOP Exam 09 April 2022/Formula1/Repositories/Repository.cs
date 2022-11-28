namespace Formula1.Repositories
{
    using Formula1.Repositories.Contracts;
    using System.Collections.Generic;

    public abstract class Repository<T> : IRepository<T>
    {
        private ICollection<T> models;

        protected Repository()
        {
            models = new List<T>();
        }

        public IReadOnlyCollection<T> Models => models as IReadOnlyCollection<T>;

        public void Add(T model) => models.Add(model);

        public abstract T FindByName(string name);

        public bool Remove(T model) => models.Remove(model);
    }
}
