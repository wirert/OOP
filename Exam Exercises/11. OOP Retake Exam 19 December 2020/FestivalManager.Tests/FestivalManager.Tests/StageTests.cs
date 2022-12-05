// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{
	using FestivalManager.Entities;
	using NUnit.Framework;
    using System;
	using System.Linq;

	[TestFixture]
	public class StageTests
    {
		private Performer performer;
		private Stage stage;
		private Song song;
		private string firstName;
		private string lastName;
		private int age;
		private TimeSpan duration;
		private string songName;

        [SetUp]
		public void SetUp()
		{
			firstName = "Test";
			lastName = "Tester";
			age = 88;
			stage = new Stage();
			performer = new Performer(firstName, lastName, age);
			duration = TimeSpan.FromSeconds(100);
			songName = "Song";
			song = new Song(songName, duration);
		}

		[Test]
	    public void CtorTest()
	    {
			Assert.AreEqual(0, stage.Performers.Count);			
		}

		[Test]
		public void AddPerformerThrowIfNull()
		{
			Assert.Throws<ArgumentNullException>(() => stage.AddPerformer(null));
		}

        [Test]
        public void AddPerformerThrowIfUnderAge()
        {
			int invalidAge = 17;

            Assert.Throws<ArgumentException>(() => stage.AddPerformer(new Performer("1", "2", invalidAge)));
        }

        [Test]
        public void AddPerformerTest()
        {
			stage.AddPerformer(performer);

            Assert.AreEqual(1, stage.Performers.Count);           
        }

        [Test]
        public void AddSongThrowIfNull()
        {
            Assert.Throws<ArgumentNullException>(() => stage.AddSong(null));
        }

        [Test]
        public void AddSongThrowIfShorterThanMinute()
        {
			duration = TimeSpan.FromSeconds(59);

            Assert.Throws<ArgumentException>(() => stage.AddSong(new Song(songName, duration)));
        }

        [Test]
        public void AddSongToPerformerTest()
        {
			stage.AddPerformer(performer);
            stage.AddSong(song);

			string output = stage.AddSongToPerformer(songName, performer.FullName);
			string expectedOutput = $"{song} will be performed by {performer.FullName}";

			Assert.AreEqual(expectedOutput, output);
			Assert.AreEqual(song, performer.SongList.First());
        }

		[Test]
		public void AddSongToPerformerThrowIfNoPerformer()
		{
            stage.AddSong(song);

			Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer(songName, performer.FullName));
        }

        [Test]
        public void AddSongToPerformerThrowIfNoSong()
        {
            stage.AddPerformer(performer);

            Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer(songName, performer.FullName));
        }

		[Test]
		public void Play_Test()
		{
            stage.AddPerformer(performer);
            stage.AddSong(song);
			stage.AddSongToPerformer(songName, performer.FullName);

            string expectedOutput = $"1 performers played 1 songs";

			Assert.AreEqual(expectedOutput, stage.Play());
        }
    }
}