namespace CarRacing.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Racers.Contracts;
    using Utilities.Messages;

    public class RacerRepository : IRepository<IRacer>
    {
        private ICollection<IRacer> racers;

        public IReadOnlyCollection<IRacer> Models => racers as IReadOnlyCollection<IRacer>;

        public void Add(IRacer model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddRacerRepository);
            }

            racers.Add(model);
        }

        public IRacer FindBy(string property) => racers.FirstOrDefault(c => c.Username == property);

        public bool Remove(IRacer model) => racers.Remove(model);
    }
}
