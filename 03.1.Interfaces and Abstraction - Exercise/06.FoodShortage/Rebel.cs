namespace FoodShortage
{
    public class Rebel : IBuyer
    {
        private string name;
        private int age;
        private string group;
        private int food;

        public Rebel(string name, int age, string group)
        {
            this.name = name;
            this.age = age;
            this.group = group;
            this.food = 0;
        }

        public string Name => name;
        public int Food => food;
        public int Age => age;
        public string Group => group;

        public void BuyFood() => food += 5;
        
    }
}
