namespace Easter.Models.Workshops
{
    using System.Linq;

    using Contracts;
    using Bunnies.Contracts;
    using Eggs.Contracts;

    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            var dye = bunny.Dyes.FirstOrDefault(d => !d.IsFinished());

            while (bunny.Energy > 0 && dye != null)
            {
                bunny.Work();
                dye.Use();

                if (dye.IsFinished())
                {  
                    bunny.Dyes.Remove(dye);
                    dye = bunny.Dyes.FirstOrDefault(d => !d.IsFinished());
                }
                                
                egg.GetColored();

                if (egg.IsDone())
                {
                    return;
                }
            }
        }
    }
}
