namespace NavalVessels.Models.Vessels
{
    using NavalVessels.Models.Contracts;
    using System;

    public class Battleship : Vessel, IBattleship
    {
        private const double Initial_ArmorTickness = 300;

       // private bool sonarMode;

        public Battleship(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, Initial_ArmorTickness)
        {
        }

        public bool SonarMode { get; private set; }

        public override void RepairVessel() => ArmorThickness = Initial_ArmorTickness;

        public void ToggleSonarMode()
        {
            SonarMode = !SonarMode;

            if (SonarMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 5;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 5;
            }
        }

        public override string ToString()
        {
            string modeString = SonarMode ? "ON" : "OFF";

            return base.ToString() + Environment.NewLine + $" *Sonar mode: {modeString}";
        }
    }
}
