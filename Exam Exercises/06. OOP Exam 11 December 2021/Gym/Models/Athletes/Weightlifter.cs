namespace Gym.Models.Athletes
{
    using Gym.Utilities.Messages;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Weightlifter : Athlete
    {
        private const int InitialStamina = 50;
        private const int MaximumStamina = 100;


        public Weightlifter(string fullName, string motivation, int numberOfMedals) 
            : base(fullName, motivation, numberOfMedals, InitialStamina)
        {
        }

        public override void Exercise()
        {
            Stamina += 10;

            if (Stamina > MaximumStamina)
            {
                Stamina = MaximumStamina;

                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }
        }
    }
}
