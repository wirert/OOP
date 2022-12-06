using NUnit.Framework;
using System;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private const string ItemId = "id";

        private BankVault vault;
        private Item item;
        private string cell;

        [SetUp]
        public void Setup()
        {
            vault = new BankVault();
            item = new Item("Name", ItemId);
        }

        [Test]
        public void Ctor_Test()
        {
            Assert.IsTrue(vault.VaultCells["A1"] == null);
        }

        [Test]
        public void AddItem_Test()
        {
            cell = "A1";    
            string expectedOutput = $"Item:{ItemId} saved successfully!";

            Assert.AreEqual(expectedOutput, vault.AddItem(cell, item));
            Assert.AreEqual(item, vault.VaultCells["A1"]);
        }

        [Test]
        public void AddItemShouldThrowIfCellInvalid()
        {
            cell = "InvalidCell";

            Assert.Throws<ArgumentException>(() => vault.AddItem(cell, item));
        }

        [Test]
        public void AddItemShouldThrowIfCellOccupied()
        {
            cell = "A1";
            vault.AddItem(cell, item);

            Assert.Throws<ArgumentException>(() => vault.AddItem(cell, new Item("Pesho", "345")));
        }

        [Test]
        public void AddItemShouldThrowIfItemWithSameIdAlreadyInVault()
        {
            cell = "A1";
            vault.AddItem(cell, item);

            string freeCell = "A2";

            Assert.Throws<InvalidOperationException>(() => vault.AddItem(freeCell, new Item("Pesho", ItemId)));
        }

        [Test]
        public void RemoveItem_Test()
        {
            cell = "A1";
            vault.AddItem(cell, item);
            string expectedOutput = $"Remove item:{ItemId} successfully!";

            Assert.AreEqual(expectedOutput,vault.RemoveItem(cell, item));
            Assert.IsTrue(vault.VaultCells[cell] == null);
        }

        [Test]
        public void RemoveItemShouldThrowIfCellInvalid()
        {
            cell = "InvalidCell";

            Assert.Throws<ArgumentException>(() => vault.RemoveItem(cell, item));
        }

        [Test]
        public void RemoveItemShouldThrowIfNoSuchItemInCell()
        {
            cell = "A1";
            vault.AddItem(cell, item);

            Assert.Throws<ArgumentException>(() => vault.RemoveItem(cell, new Item("Pesho", "InvalidId")));
        }
    }
}