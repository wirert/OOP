namespace MilitaryElite.Models
{
    using Interfaces;

    public class Repair : IRepair
    {
        public Repair(string partName, int hours)
        {
            PartName = partName;
            Hours = hours;
        }

        public string PartName { get; set; }
        public int Hours { get; set; }

        public override string ToString() => $"Part Name: {PartName} Hours Worked: {Hours}";        
    }
}
