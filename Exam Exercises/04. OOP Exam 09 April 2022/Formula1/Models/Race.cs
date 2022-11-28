namespace Formula1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Contracts;
    using Utilities;

    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;
        private List<IPilot> pilots;

        public Race(string raceName, int numberOfLaps)
        {
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
            pilots= new List<IPilot>();
        }

        public string RaceName
        {
            get => raceName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRaceName, value));
                }
                raceName = value;
            }
        }

        public int NumberOfLaps
        {
            get => numberOfLaps;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidLapNumbers, value));
                }
                numberOfLaps = value;
            }
        }

        public bool TookPlace { get; set; }

        public ICollection<IPilot> Pilots => pilots.AsReadOnly();
        public void AddPilot(IPilot pilot) => this.pilots.Add(pilot);

        public string RaceInfo()
        {
            StringBuilder sb = new StringBuilder();
            string place = TookPlace ? "Yes" : "No";

            sb.AppendLine($"The {this.RaceName} race has:")
            .AppendLine($"Participants: {Pilots.Count}")
            .AppendLine($"Number of laps: {NumberOfLaps}")
            .AppendLine($"Took place: {place}");

            return sb.ToString().Trim();
        }
    }
}
