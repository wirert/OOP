using INStock.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace INStock
{
    public class ProductStock : IProductStock
    {
        private IList<IProduct> products;

        public ProductStock(IList<IProduct> products)
        {
            this.products = products;
        }

        public IProduct this[int index] 
        {
            get
            {
                if (index < 0 || index >= products.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index");
                }

                return products[index];
            }
            set 
            {
                products[index] = value;
            }         
        }

        public int Count => products.Count;

        public void Add(IProduct product)
        {
            products.Add(product);
        }

        public bool Contains(IProduct product)
        {
            throw new NotImplementedException();
        }

        public IProduct Find(int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProduct> FindAllByPrice(double price)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProduct> FindAllByQuantity(int quantity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProduct> FindAllInRange(double lo, double hi)
        {
            throw new NotImplementedException();
        }

        public IProduct FindByLabel(string label)
        {
            throw new NotImplementedException();
        }

        public IProduct FindMostExpensiveProduct()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IProduct> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(IProduct product)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
