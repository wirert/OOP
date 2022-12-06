namespace EasterRaces.Repositories.Entities
{
    using System.Collections.Generic;

    using Contracts;

    public abstract class Repository<T> : IRepository<T>
    {
        private readonly ICollection<T> repository;

        public Repository()
        {
            repository= new HashSet<T>();
        }

        public void Add(T model) => repository.Add(model);

        public IReadOnlyCollection<T> GetAll() => repository as IReadOnlyCollection<T>;

        public abstract T GetByName(string name);        

        public bool Remove(T model) => repository.Remove(model);
    }
}
