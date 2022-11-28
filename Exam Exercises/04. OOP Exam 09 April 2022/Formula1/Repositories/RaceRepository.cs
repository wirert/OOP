namespace Formula1.Repositories
{
    using System.Linq;
    using Models.Contracts;

    public class RaceRepository : Repository<IRace>
    {      
        public override IRace FindByName(string name) => Models.FirstOrDefault(r => r.RaceName == name);
    }
}
