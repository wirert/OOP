namespace Formula1.Repositories
{
    using System.Linq;
    using Models.Contracts;

    public class FormulaOneCarRepository : Repository<IFormulaOneCar>
    {
        public override IFormulaOneCar FindByName(string name) => Models.FirstOrDefault(c => c.Model == name);
    }
}
