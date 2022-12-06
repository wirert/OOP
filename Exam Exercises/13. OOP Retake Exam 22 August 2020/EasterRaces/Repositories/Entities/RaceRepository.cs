namespace EasterRaces.Repositories.Entities
{
    using System.Linq;

    using Models.Races.Contracts;

    public class RaceRepository : Repository<IRace>
    {
        public override IRace GetByName(string name) => GetAll().FirstOrDefault(c => c.Name == name);
    }
}
