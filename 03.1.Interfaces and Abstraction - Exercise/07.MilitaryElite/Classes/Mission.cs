﻿using System;

namespace MilitaryElite
{
    public class Mission : IMission
    {        

        public Mission(string codeName, string state)
        {
            CodeName = codeName;
            State = state;
        }

        public string CodeName { get; private set; }
        public string State { get; private set; }
        

        public void CompleteMission() => State = "Finished";


        public override string ToString() => $"Code Name: {CodeName} State: {State}";
    }
}
