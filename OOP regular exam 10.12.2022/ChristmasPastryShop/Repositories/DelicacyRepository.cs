namespace ChristmasPastryShop.Repositories
{
    using System.Collections.Generic;

    using Contracts;
    using Models.Delicacies.Contracts;

    public class DelicacyRepository : IRepository<IDelicacy>
    {
        private readonly ICollection<IDelicacy> delicacies;

        public DelicacyRepository()
        {
            delicacies= new HashSet<IDelicacy>();
        }

        public IReadOnlyCollection<IDelicacy> Models => delicacies as IReadOnlyCollection<IDelicacy>;

        public void AddModel(IDelicacy model) => delicacies.Add(model);
    }
}