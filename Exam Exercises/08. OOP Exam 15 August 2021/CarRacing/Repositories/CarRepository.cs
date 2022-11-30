namespace CarRacing.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarRacing.Utilities.Messages;
    using Contracts;
    using Models.Cars.Contracts;

    public class CarRepository : IRepository<ICar>
    {
        private ICollection<ICar> cars;

        public CarRepository()
        {
            cars = new List<ICar>();
        }

        public IReadOnlyCollection<ICar> Models => cars as IReadOnlyCollection<ICar>;

        public void Add(ICar model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddCarRepository);
            }

            cars.Add(model);
        }

        public ICar FindBy(string property) => cars.FirstOrDefault(c => c.VIN == property);

        public bool Remove(ICar model) => cars.Remove(model);
    }
}
