using System;

namespace MilitaryElite
{
    public class Mission
    {        
        private string state;

        public Mission(string codeName, string state)
        {
            CodeName = codeName;
            State = state;
        }

        public string CodeName { get; private set; }
        public string State
        {
            get => state;
            set
            {
                if (value != "inProgress" && value != "Finished")
                {
                    throw new ArgumentException();
                }

                state = value;
            }
        }

        public void CompleteMission() => State = "Finished";
        

        public override string ToString() => $"Code Name: {CodeName} State: {State}";        
    }
}
