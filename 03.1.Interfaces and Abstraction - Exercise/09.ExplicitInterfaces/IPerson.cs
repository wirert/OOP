namespace ExplicitInterfaces
{
    public interface IPerson
    {
        public string Name { get; }
        public int Age { get; set; }

        public void GetName();
    }
}
