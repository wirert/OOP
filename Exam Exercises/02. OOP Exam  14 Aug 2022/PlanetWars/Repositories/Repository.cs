namespace PlanetWars.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;

    public abstract class Repository<T> : IRepository<T>
    {
        private ICollection<T> models;

        public Repository()
        {
            this.models = new List<T>();
        }

        public IReadOnlyCollection<T> Models => (IReadOnlyCollection<T>)this.models;

        public void AddItem(T model) => models.Add(model);

        public T FindByName(string name) => Models.FirstOrDefault(w => w.GetType().Name == name);

        public bool RemoveItem(string name)
        {
            T model = this.FindByName(name);

            if (model != null)
            {
                models.Remove(model);
                return true;
            }

            return false;
        }
    }
}
