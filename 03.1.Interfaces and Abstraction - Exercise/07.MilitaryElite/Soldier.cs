using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite
{
    public abstract class Soldier
    {
        private int id;
        private string firstName;
        private string lastName;

        public Soldier(int id, string firstName, string lastName)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public int Id => id;
        public string FirstName => firstName;
        public string LastName => lastName;
    }
}
