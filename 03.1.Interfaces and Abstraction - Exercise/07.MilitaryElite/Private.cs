namespace MilitaryElite
{
    public class Private : Soldier
    {
        private decimal salary;

        public Private(int id, string firstName, string lastName, decimal salary) : base(id, firstName, lastName)
        {
            this.salary = salary;
        }

        public decimal Salary => salary;

        public override string ToString()
        {
            return $"Name: {this.FirstName} {LastName} Id: {Id} Salary: {this.salary}";
        }
    }
}
