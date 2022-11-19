namespace _03BarracksFactory.Core.Commands
{
    using Attributes;
    using Contracts;

    public class RetireCommand : Command
    {
        [Inject]
        private IRepository repository;
        
        public RetireCommand(string[] data) : base(data)
        {
        }

        public override string Execute()
        {
            string unitType = Data[1];
            repository.RemoveUnit(unitType);

            return $"{unitType} retired!";
        }
    }
}
