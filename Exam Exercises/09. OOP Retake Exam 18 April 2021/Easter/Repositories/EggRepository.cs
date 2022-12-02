namespace Easter.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Eggs.Contracts;

    public class EggRepository : IRepository<IEgg>
    {
        private readonly ICollection<IEgg> eggs;

        public EggRepository()
        {
            eggs= new HashSet<IEgg>();
        }

        public IReadOnlyCollection<IEgg> Models => eggs as IReadOnlyCollection<IEgg>;

        public void Add(IEgg model) => eggs.Add(model);

        public IEgg FindByName(string name) => eggs.FirstOrDefault(e => e.Name == name);

        public bool Remove(IEgg model) => eggs.Remove(model);
    }
}
