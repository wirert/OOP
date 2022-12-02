namespace NavalVessels.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Contracts;

    public class VesselRepository : IRepository<IVessel>
    {
        //private List<IVessel> vessels;
        private readonly ICollection<IVessel> vessels;

        public VesselRepository()
        {
            vessels= new HashSet<IVessel>();
        }

        public IReadOnlyCollection<IVessel> Models => (IReadOnlyCollection<IVessel>)vessels;

        public void Add(IVessel model) => vessels.Add(model);

        public IVessel FindByName(string name) => vessels.FirstOrDefault(v => v.Name == name);

        public bool Remove(IVessel model) => vessels.Remove(model);
    }
}
