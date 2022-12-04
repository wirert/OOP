namespace Easter.Core
{
    using Contracts;
    using Easter.Models.Bunnies;
    using Easter.Models.Dyes;
    using Easter.Models.Eggs;
    using Easter.Models.Workshops;
    using Easter.Repositories;
    using Easter.Utilities.Messages;
    using Models.Bunnies.Contracts;
    using Models.Eggs.Contracts;
    using Repositories.Contracts;
    using System;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private IRepository<IBunny> bunnies;
        private IRepository<IEgg> eggs;

        public Controller()
        {
            bunnies = new BunnyRepository();
            eggs = new EggRepository();
        }

        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny = null;

            if (bunnyType == "HappyBunny")
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunny = new SleepyBunny(bunnyName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }

            bunnies.Add(bunny);

            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            var bunny = bunnies.FindByName(bunnyName);

            if (bunny == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }

            bunny.AddDye(new Dye(power));

            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            eggs.Add(new Egg(eggName, energyRequired));

            return string.Format(OutputMessages.EggAdded, eggName);
        }


        public string ColorEgg(string eggName)
        {
            IEgg egg = eggs.FindByName(eggName);

            var suitableBunnies = bunnies.Models.Where(b => b.Energy >= 50).OrderByDescending(b => b.Energy);

            if (!suitableBunnies.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }

            var workshop = new Workshop();

            foreach (var bunny in suitableBunnies)
            {
                workshop.Color(egg, bunny);

                if (bunny.Energy == 0)
                {
                    bunnies.Remove(bunny);
                }

                if (egg.IsDone())
                {
                    return string.Format(OutputMessages.EggIsDone, eggName);
                }
            }

            return string.Format(OutputMessages.EggIsNotDone, eggName);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            int colouredEggs = eggs.Models.Where(e => e.IsDone()).Count();
            sb.AppendLine($"{colouredEggs} eggs are done!")
                .AppendLine("Bunnies info:");

            foreach (var bunny in bunnies.Models)
            {
                sb.AppendLine($"Name: {bunny.Name}")
                    .AppendLine($"Energy: {bunny.Energy}")
                    .AppendLine($"Dyes: {bunny.Dyes.Count} not finished");
            }

            return sb.ToString().Trim();
        }
    }
}