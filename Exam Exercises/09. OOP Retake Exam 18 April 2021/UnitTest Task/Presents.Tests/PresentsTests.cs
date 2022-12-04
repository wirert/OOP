namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class PresentsTests
    {
        private const string DEF_PRESENT_NAME = "Present name";
        private const double DEF_PRESENT_MAGIC = 10.2;

        private Present present;
        private Bag bag;

        [SetUp]
        public void Setup()
        {
            present = new Present(DEF_PRESENT_NAME, DEF_PRESENT_MAGIC);
            bag = new Bag();
            bag.Create(present);
        }

        [Test]
        public void Ctor_ShouldInitializeEmptyCollection()
        {
            bag = new Bag();

            Assert.AreEqual(0, bag.GetPresents().Count);            
        }

        [Test]
        public void Create_ShoudAddPresentToCollection()
        {
            Assert.AreEqual(1, bag.GetPresents().Count);
            Assert.AreEqual(DEF_PRESENT_NAME, bag.GetPresents().First().Name);
            Assert.AreSame(present, bag.GetPresent(DEF_PRESENT_NAME));
        }

        [Test]
        public void Create_ShoudReturnMessage()
        {
            string name = "Present Two";
            string expectedOutputMessage = $"Successfully added present {name}.";

            Assert.AreEqual(expectedOutputMessage, bag.Create(new Present(name, 44.5)));
        }


        [Test]
        public void Create_ShoudThrowIfPresentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => bag.Create(null), "Present is null");
        }

        [Test]
        public void Create_ShoudThrowIfPresentIsAlreadyInCollection()
        {     
            Assert.Throws<InvalidOperationException>(() => bag.Create(present), "This present already exists!");
        }

        [Test]
        public void Remove_Test()
        {
            bag.Remove(present);

            Assert.AreEqual(0, bag.GetPresents().Count);
            Assert.IsTrue(bag.GetPresent(DEF_PRESENT_NAME) == null);
        }

        [Test]
        public void GetPresentWithLeastMagic_Test()
        {            
            double leastMagic = 669.5;
            Present present2 = new Present("New present", leastMagic);
                       
            bag.Create(present2);
            
            double expectedLeastMagic = Math.Min(present.Magic, present2.Magic);
            Assert.AreEqual(expectedLeastMagic, bag.GetPresentWithLeastMagic().Magic);
        }

        [Test]
        public void GetPresent_ShoudReturnPresentWithGivenName()
        {            
            Present present2 = new Present("Another", 7.0);
            
            bag.Create(present2);

            Assert.AreEqual(DEF_PRESENT_NAME, bag.GetPresent(DEF_PRESENT_NAME).Name);
            Assert.AreSame(bag.GetPresent("Missing name"), null);
        }

        [Test]
        public void Collection_IsReadOnly_Test()
        {           
            var presents = bag.GetPresents();
            presents = new List<Present>();

            Assert.AreEqual(1, bag.GetPresents().Count);
        }

        [Test]
        public void Change_Present_Data_Test()
        {            
            string newName = "Another Name";
            double newMagic = 66.5;

            present.Name = newName;
            present.Magic = newMagic;

            Assert.AreEqual(newName, present.Name);
            Assert.AreEqual(newMagic, present.Magic);
        }
    }
}
