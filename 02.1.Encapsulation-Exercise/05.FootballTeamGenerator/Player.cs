using System;

namespace _05.FootballTeamGenerator
{
    public class Player
    {
		private string name;
		private int endurance;
		private int sprint;
		private int dribble;
		private int passing;
		private int shooting;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            Name = name;
            Endurance = endurance;
            Sprint = sprint;
            Dribble = dribble;
            Passing = passing;
            Shooting = shooting;
        }

        public string Name
		{
			get { return name; }
			private set 
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }

                name = value; 
            }
		}
        public int Endurance
        {
            get { return endurance; }
            private set { endurance = SetStatIfValid("Endurance", value); }
        }
        public int Sprint
        {
            get { return sprint; }
            private set { sprint = SetStatIfValid("Sprint", value); }
        }
        public int Dribble
        {
            get { return dribble; }
            private set { dribble = SetStatIfValid("Dribble", value); }
        }
        public int Passing
        {
            get { return passing; }
            private set { passing = SetStatIfValid("Passing", value); }
        }
        public int Shooting
        {
            get { return shooting; }
            private set { shooting = SetStatIfValid("Shooting", value); }
        }
        public int SkillLevel => (int)Math.Round((Endurance + Sprint + Dribble + Passing + Shooting) / 5.0);

        private int SetStatIfValid(string statName, int value)
		{
			if (value < 0 || value > 100)
			{
				throw new ArgumentException($"{statName} should be between 0 and 100.");
			}

			return value;
		}       
    }
}
