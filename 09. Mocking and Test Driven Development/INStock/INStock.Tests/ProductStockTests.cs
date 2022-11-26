namespace INStock.Tests
{
    using INStock.Contracts;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductStockTests
    {
        [Test]
        public void AddProductTest()
        {
            Mock<IProduct> mockProduct = new Mock<IProduct>();

            IList<IProduct> products = new List<IProduct>();

            //Mock<IList<IProduct>> mockProducts= new Mock<IList<IProduct>>();
            ProductStock stock = new ProductStock(products);
            stock.Add(mockProduct.Object);

            Assert.AreEqual(1, stock.Count);
        }

        [Test]
        public void FindAllByPriceShouldReturnEmptyCollectionIfNoneFinded()
        {
            ProductStock stock = new ProductStock(new List<IProduct>());

            Assert.AreEqual(0, stock.FindAllByPrice(10).ToList<IProduct>().Count);
        }
    }
}
