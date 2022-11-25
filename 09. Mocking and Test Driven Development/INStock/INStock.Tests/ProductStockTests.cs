namespace INStock.Tests
{
    using INStock.Contracts;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;

    public class ProductStockTests
    {
        [Test]
        public void AddProductTest()
        {
            Mock<IProduct> mockProduct = new Mock<IProduct>();

            List<IProduct> products = new List<IProduct>();

            //Mock<IList<IProduct>> mockProducts= new Mock<IList<IProduct>>();
            ProductStock stock = new ProductStock(products);
            stock.Add(mockProduct.Object);

            Assert.AreEqual(1, stock.Count);
        }
    }
}
