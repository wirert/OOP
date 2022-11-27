namespace Heroes.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Contracts;

    public class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> weapons;

        public WeaponRepository()
        {
            weapons= new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models => this.weapons;

        public void Add(IWeapon model) => this.weapons.Add(model);

        public IWeapon FindByName(string name) => weapons.FirstOrDefault(w => w.Name == name);

        public bool Remove(IWeapon model) => weapons.Remove(model);
    }
}
