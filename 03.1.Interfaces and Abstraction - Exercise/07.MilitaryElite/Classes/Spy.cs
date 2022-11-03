using System;

namespace MilitaryElite
{
    public class Spy : Soldier, ISpy
    {  
        public Spy(int id, string firstName, string lastName, int codeNumber) : base(id, firstName, lastName)
        {
            this.CodeNumber = codeNumber;
        }

        public int CodeNumber { get; set; }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName} Id: {Id}" + Environment.NewLine + $"Code Number: {CodeNumber}";
        }
    }
}
