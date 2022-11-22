namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void ConstructorShouldSetInputDataDB(int[] data)
        {
            Database db = new Database(data);
            int expectedCount = data.Length;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddMethodShouldThrowExceptionIfElementsAre16InDB()
        {
            Database db = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            Assert.Throws<InvalidOperationException>(() =>
            {
                db.Add(17);
            }, "Array's capacity must be exactly 16 integers!");
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 })]
        public void AddMethodShouldAddElementToDB(int[]  data)
        {
            int numToAdd = 55;
            var db = new Database(data);
            db.Add(numToAdd);
            int expectedCount = data.Length + 1;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
            Assert.AreEqual(numToAdd, db.Fetch()[db.Count - 1]);
        }

        [Test]
        public void RemoveMethodShouldThrowExeptionIfDataBaseIsEmpty()
        {
            var db = new Database();

            Assert.Throws<InvalidOperationException>(() =>
            {
                db.Remove();
            }, "The collection is empty!");
        }

        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,16 })]
        public void RemoveMethodShouldRemoveOneElementFromDb(int[] data)
        {
            var db = new Database(data);
            db.Remove();
            Assert.AreEqual(data.Length - 1, db.Count);
        }

        [Test]
        public void RemoveMethodShouldRemoveTheLastElementFromDb()
        {
            var db = new Database(1,2,3,4,5);
            db.Remove();
            int expected = 4;
            int actual = db.Fetch()[db.Count - 1];

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 , 16})]
        public void FetchMethodShouldReturnTheDBElementsInActualOrder(int[] data)
        {
            var db = new Database(data);

            CollectionAssert.AreEqual(data, db.Fetch());
        }
    }
}
