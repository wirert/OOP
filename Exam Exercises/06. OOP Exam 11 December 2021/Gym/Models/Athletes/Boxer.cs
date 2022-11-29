namespace Gym.Models.Athletes
{
    using Gym.Utilities.Messages;
    using System;

    public class Boxer : Athlete
    {
        private const int InitialStamina = 60;
        private const int MaximumStamina = 100;

        public Boxer(string fullName, string motivation, int numberOfMedals) 
            : base(fullName, motivation, numberOfMedals, InitialStamina)
        {
        }

        public override void Exercise()
        {
            Stamina += 15;

            if (Stamina > MaximumStamina)
            {
                Stamina = MaximumStamina;

                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }
        }
    }
}
