namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        [TestCase(0)]
        [TestCase(8)]
        [TestCase(16)]
        public void ConstructorShouldSetInputDataDB(int personsToAdd)
        {
            List<Person> dataList = new List<Person>();

            for (int i = 0; i < personsToAdd; i++)
            {
                string name = ((char)(97 + i)).ToString();
                dataList.Add(new Person(i + 1, name));
            }

            var data = dataList.ToArray();

            Database db = new Database(data);
            int expectedCount = data.Length;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(17)]
        [TestCase(29)]
        public void ConstructorShouldThrowExceptionIfArgumetsMoreThan16(int elements)
        {
            int numElements = elements;
            Person[] data = new Person[numElements];

            for (int i = 0; i < numElements; i++)
            {
                string name = ((char)(97 + i)).ToString();
                data[i] = new Person(i + 1, name);
            }

            Assert.Throws<ArgumentException>(() =>
            {
                Database db = new Database(data);
            }, "Provided data length should be in range [0..16]!");
        }

        [Test]
        public void AddMethodShouldThrowExceptionIfElementsAre16InDB()
        {
            Person[] data = new Person[16];

            for (int i = 0; i < 16; i++)
            {
                string name = ((char)(97 + i)).ToString();
                data[i] = new Person(i + 1, name);
            }

            Database db = new Database(data);

            Assert.Throws<InvalidOperationException>(() =>
            {
                db.Add(new Person(243, "Pesho"));
            }, "Array's capacity must be exactly 16 integers!");
        }

        [TestCase(0)]
        [TestCase(8)]
        [TestCase(15)]
        public void AddMethodShouldAddElementToDB(int persons)
        {
            int numElements = persons;
            Person[] data = new Person[numElements];

            for (int i = 0; i < numElements; i++)
            {
                string name = ((char)(97 + i)).ToString();
                data[i] = new Person(i + 1, name);
            }

            long id = 55;
            string userName = "pesho";
            var db = new Database(data);
            var newPerson = new Person(id, userName);
            db.Add(newPerson);
            int expectedCount = data.Length + 1;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddedPersonShouldHaveUniqueUserNameElseThrowException()
        {
            long id1 = 55;
            long id2 = 60;

            string userName = "pesho";
            var person1 = new Person(id1, userName);
            var person2 = new Person(id2, userName);
            var db = new Database(person1);

            Assert.Throws<InvalidOperationException>(() => db.Add(person2), "There is already user with this username!");
        }

        [Test]
        public void AddedPersonShouldHaveUniqueIdElseThrowException()
        {
            long id = 55;

            string userName1 = "pesho";
            string userName2 = "gosho";
            var person1 = new Person(id, userName1);
            var person2 = new Person(id, userName2);
            var db = new Database(person1);

            Assert.Throws<InvalidOperationException>(() => db.Add(person2), "There is already user with this Id!");
        }

        [Test]
        public void RemoveMethodShouldThrowExeptionIfDataBaseIsEmpty()
        {
            var db = new Database();

            Assert.Throws<InvalidOperationException>(() => db.Remove());
        }

        [TestCase(1)]
        [TestCase(8)]
        [TestCase(16)]
        public void RemoveMethodShouldRemoveOneElementFromDb(int persons)
        {
            int numElements = persons;
            Person[] data = new Person[numElements];

            for (int i = 0; i < numElements; i++)
            {
                string name = ((char)(97 + i)).ToString();
                data[i] = new Person(i + 1, name);
            }

            var db = new Database(data);
            db.Remove();
            Assert.AreEqual(data.Length - 1, db.Count);
        }

        [TestCase("")]
        [TestCase(null)]
        public void FindByUserNameShouldThrowExceptionIfNameIsNullOrEmpty(string nameToFind)
        {
            int numElements = 8;
            Person[] data = new Person[numElements];

            for (int i = 0; i < numElements; i++)
            {
                string name = ((char)(97 + i)).ToString();
                data[i] = new Person(i + 1, name);
            }

            var db = new Database(data);

            Assert.Throws<ArgumentNullException>(() => db.FindByUsername(nameToFind), "Username parameter is null!");
        }

        [Test]
        public void FindByUserNameShouldThrowExceptionIfThereIsNoSuchUserInDB()
        {
            string nameToFind = "Pesho";
            int numElements = 8;
            Person[] data = new Person[numElements];

            for (int i = 0; i < numElements; i++)
            {
                string name = ((char)(97 + i)).ToString();
                data[i] = new Person(i + 1, name);
            }

            var db = new Database(data);

            Assert.Throws<InvalidOperationException>(() => db.FindByUsername(nameToFind)
            , "No user is present by this username!");
        }

        [TestCase("a")]
        [TestCase("h")]
        [TestCase("d")]
        public void FindByUserNameShouldReturnTheUserWithGivenNameFromDB(string userName)
        {
            string nameToFind = userName;
            int numElements = 8;
            Person[] data = new Person[numElements];

            for (int i = 0; i < numElements; i++)
            {
                string name = ((char)(97 + i)).ToString();
                data[i] = new Person(i + 1, name);
            }
            var db = new Database(data);

            string findedName = db.FindByUsername(userName).UserName;

            Assert.AreEqual(userName, findedName);
        }

        [Test]
        public void FindByIdShouldThrowExceptionIfIdIsBelowZero()
        {
            long id = -1;
            int numElements = 8;
            Person[] data = new Person[numElements];

            for (int i = 0; i < numElements; i++)
            {
                string name = ((char)(97 + i)).ToString();
                data[i] = new Person(i + 1, name);
            }

            var db = new Database(data);

            Assert.Throws<ArgumentOutOfRangeException>(() => db.FindById(id), "Id should be a positive number!");
        }

        [Test]
        public void FindByIdShouldThrowExceptionIfThereIsNoSuchId()
        {
            long id = 55;
            int numElements = 8;
            Person[] data = new Person[numElements];

            for (int i = 0; i < numElements; i++)
            {
                string name = ((char)(97 + i)).ToString();
                data[i] = new Person(i + 1, name);
            }

            var db = new Database(data);

            Assert.Throws<InvalidOperationException>(() => db.FindById(id), "No user is present by this ID!");
        }

        [Test]
        public void FindByIdShouldReturnThePersonWithGivenId()
        {
            long id = 4;
            int numElements = 8;
            Person[] data = new Person[numElements];

            for (int i = 0; i < numElements; i++)
            {
                string name = ((char)(97 + i)).ToString();
                data[i] = new Person(i + 1, name);
            }

            var db = new Database(data);

            Assert.AreEqual(data.First(p => p.Id == id), db.FindById(id));
        }
    }
}