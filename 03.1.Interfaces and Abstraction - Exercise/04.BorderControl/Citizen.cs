namespace BorderControl
{
    public class Citizen : IIdentifiable
    {
        private string name;
        private int age;
        private string id;

        public Citizen(string id)
        {
            this.id = id;
        }

        public Citizen(string name, int age, string id) : this(id)
        {
            this.name = name;
            this.age = age;            
        }

        public string Id => id;
        public string Name => name;
        public int Age => age;
    }
}
