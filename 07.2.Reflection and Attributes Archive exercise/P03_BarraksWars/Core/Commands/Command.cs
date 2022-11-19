namespace _03BarracksFactory.Core.Commands
{
    using Contracts;

    public abstract class Command : IExecutable
    {
        private string[] data;

        protected Command(string[] data)
        {
            Data = data;
        }

        protected string[] Data
        {
            get => data;
            private set { this.data = value; }
        }

        public abstract string Execute();
    }
}
