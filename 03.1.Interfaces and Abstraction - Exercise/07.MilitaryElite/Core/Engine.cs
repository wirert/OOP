namespace MilitaryElite.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using IO.Interfaces;
    using Interfaces;
    using Models;
    using Models.Interfaces;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private ISet<ISoldier> allSoldiers;

        public Engine()
        {
            allSoldiers = new HashSet<ISoldier>();
        }

        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            AddSoldiers();

            foreach (var soldier in this.allSoldiers)
            {
                writer.WriteLine(soldier.ToString());
            }
        }

        private void AddSoldiers()
        {
            string command;

            while ((command = reader.ReadLine()) != "End")
            {
                string[] tokens = command.Split();
                string typeSoldier = tokens[0];
                int id = int.Parse(tokens[1]);
                string firstName = tokens[2];
                string lastName = tokens[3];
                ISoldier soldier = null;

                switch (typeSoldier)
                {
                    case "Private":
                        soldier = new Private(id, firstName, lastName, decimal.Parse(tokens[4]));
                        break;
                    case "LieutenantGeneral":
                        soldier = LieutenantGeneral(allSoldiers, tokens);
                        break;
                    case "Engineer":
                        if (IsCorpsInValid(tokens[5])) continue;

                        soldier = Engineer(tokens);

                        break;
                    case "Commando":
                        if (IsCorpsInValid(tokens[5])) continue;

                        soldier = Commando(tokens);

                        break;
                    case "Spy":

                        soldier = new Spy(id, firstName, lastName, int.Parse(tokens[4]));

                        break;
                }

                allSoldiers.Add(soldier);
            }
        }

        private Soldier LieutenantGeneral(ISet<ISoldier> privates, string[] tokens)
        {
            int id = int.Parse(tokens[1]);
            string firstName = tokens[2];
            string lastName = tokens[3];
            decimal salary = decimal.Parse(tokens[4]);
            ISet<IPrivate> privatesInCommand = new HashSet<IPrivate>();

            for (int i = 5; i < tokens.Length; i++)
            {
                IPrivate privateToAdd = (IPrivate)privates.FirstOrDefault(p => p.Id == int.Parse(tokens[i]));

                if (privateToAdd != null)
                {
                    privatesInCommand.Add(privateToAdd);
                }
            }

            return new LieutenantGeneral(id, firstName, lastName, salary, privatesInCommand);
        }

        private bool IsCorpsInValid(string corps) => corps != "Airforces" && corps != "Marines";

        private Soldier Engineer(string[] tokens)
        {
            int id = int.Parse(tokens[1]);
            string firstName = tokens[2];
            string lastName = tokens[3];
            decimal salary = decimal.Parse(tokens[4]);
            string corps = tokens[5];
            ISet<IRepair> repairs = new HashSet<IRepair>();

            for (int i = 6; i < tokens.Length - 1; i += 2)
            {
                repairs.Add(new Repair(tokens[i], int.Parse(tokens[i + 1])));
            }

            return new Engineer(id, firstName, lastName, salary, corps, repairs);
        }

        private Soldier Commando(string[] tokens)
        {
            int id = int.Parse(tokens[1]);
            string firstName = tokens[2];
            string lastName = tokens[3];
            decimal salary = decimal.Parse(tokens[4]);
            string corps = tokens[5];
            ISet<IMission> missions = new HashSet<IMission>();

            for (int i = 6; i < tokens.Length - 1; i += 2)
            {
                string codeName = tokens[i];
                string state = tokens[i + 1];

                if (state != "inProgress" && state != "Finished")
                {
                    continue;
                }

                missions.Add(new Mission(codeName, state));
            }

            return new Commando(id, firstName, lastName, salary, corps, missions);
        }
    }
}
