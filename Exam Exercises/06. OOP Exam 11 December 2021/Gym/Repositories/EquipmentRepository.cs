namespace Gym.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Equipment.Contracts;

    public class EquipmentRepository : IRepository<IEquipment>
    {
        private List<IEquipment> equipments;

        public EquipmentRepository()
        {
            this.equipments = new List<IEquipment>();
        }

        public IReadOnlyCollection<IEquipment> Models => equipments.AsReadOnly();

        public void Add(IEquipment model) => equipments.Add(model);

        public IEquipment FindByType(string type) => equipments.FirstOrDefault(e => e.GetType().Name == type);

        public bool Remove(IEquipment model) => equipments.Remove(model);
    }
}
