using NUnit.Framework;
using System;
using System.Linq;
using System.Numerics;

namespace FootballTeam.Tests
{
    [TestFixture]
    public class Tests
    {
        private const string NAME = "TeamName";
        private const string KeeperPossition = "Goalkeeper";
        private const string MidfielderPossition = "Midfielder";
        private const string ForwardPossition = "Forward";
        private const int CAPACITY = 15;

        private FootballTeam team;

        [SetUp]
        public void Setup()
        {
            team = new FootballTeam(NAME, CAPACITY);
        }

        [Test]
        public void Ctor_ShouldSetParametersProperly()
        {
            Assert.AreEqual(CAPACITY, team.Capacity);
            Assert.AreEqual(NAME, team.Name);
            Assert.AreEqual(0, team.Players.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        public void NameTrowsIfNullOrEmpty(string teamName)
        {
            Assert.Throws<ArgumentException>(() => new FootballTeam(teamName, 25));
        }

        [TestCase(14)]
        [TestCase(-5)]
        public void CapacityShouldBeMoreThan_14(int capacity)
        {
            Assert.Throws<ArgumentException>(() => new FootballTeam("Test", capacity));
        }

        [Test]
        public void AddNewPlayer_ShoudAddToPlayers()
        {
            var player = new FootballPlayer("Player", 5, ForwardPossition);
            string expectedOutput = $"Added player {player.Name} in position {player.Position} with number {player.PlayerNumber}";

            Assert.AreEqual(expectedOutput, team.AddNewPlayer(player));
            Assert.AreEqual(player, team.Players.First());
        }

        [Test]
        public void AddNewPlayer_DontAddIfNoCapacityLeft()
        {
            for (int i = 1; i < 16; i++)
            {
                team.AddNewPlayer(new FootballPlayer(NAME, i, ForwardPossition));
            }

            string expectedOutput = "No more positions available!";

            Assert.AreEqual(expectedOutput, team.AddNewPlayer(new FootballPlayer("pesho", 18, KeeperPossition)));
            Assert.AreEqual(CAPACITY, team.Players.Count);
        }

        [Test]
        public void PickPlayerReturnPlayerWithGivenName()
        {
            string playerName = "pesho";
            var player1 = new FootballPlayer(playerName, 15, ForwardPossition);
            var player2 = new FootballPlayer("gosho", 4, MidfielderPossition);

            team.Players.Add(player1);
            team.Players.Add(player2);

            Assert.AreEqual(player1, team.PickPlayer(playerName));
        }

        [TestCase(null)]
        [TestCase("")]
        public void NewPlayerThrowIfNameNullOrEmpty(string playerName)
        {
            Assert.Throws<ArgumentException>(() => new FootballPlayer(playerName, 4, ForwardPossition));
        }

        [Test]
        public void PlayerScore_ReturnGivenPlayerScore()
        {
            string playerName = "pesho";
            int playerNumber = 5;
            var player = new FootballPlayer(playerName, playerNumber, ForwardPossition);
            team.AddNewPlayer(player);

            string expectedOutput = $"{player.Name} scored and now has {player.ScoredGoals + 1} for this season!"; 

            Assert.AreEqual(expectedOutput, team.PlayerScore(playerNumber));            
        }
    }
}