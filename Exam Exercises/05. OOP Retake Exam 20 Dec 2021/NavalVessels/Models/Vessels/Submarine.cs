namespace NavalVessels.Models.Vessels
{
    using NavalVessels.Models.Contracts;
    using System;

    public class Submarine : Vessel, ISubmarine
    {
        private const double Initial_ArmorTickness = 200;

        public Submarine(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, Initial_ArmorTickness)
        {
        }

       public bool SubmergeMode { get; private set; }

        public override void RepairVessel() => ArmorThickness = Initial_ArmorTickness;

        public void ToggleSubmergeMode()
        {
            SubmergeMode = !SubmergeMode;

            if (SubmergeMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 4;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 4;
            }
        }

        public override string ToString()
        {
            string modeString = SubmergeMode ? "ON" : "OFF";

            return base.ToString() + Environment.NewLine + $" *Submerge mode: {modeString}";
        }
    }
}
