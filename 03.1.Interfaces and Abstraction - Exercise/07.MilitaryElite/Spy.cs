using System;

namespace MilitaryElite
{
    public class Spy : Soldier
    {
        private int codeNumber;

        public Spy(int id, string firstName, string lastName, int codeNumber) : base(id, firstName, lastName)
        {
            this.codeNumber = codeNumber;
        }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName} Id: {Id}" + Environment.NewLine + $"Code Number: {codeNumber}";
        }
    }
}
