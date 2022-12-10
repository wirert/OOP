namespace ChristmasPastryShop.Repositories
{
    using System.Collections.Generic;

    using Contracts;
    using Models.Cocktails.Contracts;

    public class CocktailRepository : IRepository<ICocktail>
    {
        private readonly ICollection<ICocktail> cocktails;

        public CocktailRepository()
        {
            cocktails= new HashSet<ICocktail>();
        }

        public IReadOnlyCollection<ICocktail> Models => cocktails as IReadOnlyCollection<ICocktail>;

        public void AddModel(ICocktail model) => cocktails.Add(model);
    }
}