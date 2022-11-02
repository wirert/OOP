namespace FoodShortage
{
    internal class Citizen : IBuyer
    {
        private string name;
        private int age;
        private string id;
        private string birthday;
        private int food;

        public Citizen(string name, int age, string id, string birthday)
        {
            this.name = name;
            this.age = age;
            this.id = id;
            this.birthday = birthday;
            food = 0;
        }

        public string Name => name;
        public int Food => food;

        public void BuyFood() => food += 10;        
    }
}
