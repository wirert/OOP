namespace ChristmasPastryShop.Repositories
{
    using System.Collections.Generic;

    using Contracts;
    using Models.Booths.Contracts;

    public class BoothRepository : IRepository<IBooth>
    {
        private readonly ICollection<IBooth> booths;

        public BoothRepository()
        {
            booths = new HashSet<IBooth>();
        }

        public IReadOnlyCollection<IBooth> Models => booths as IReadOnlyCollection<IBooth>;

        public void AddModel(IBooth model) => booths.Add(model);
    }
}
