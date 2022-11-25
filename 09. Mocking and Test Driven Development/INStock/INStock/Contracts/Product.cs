using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace INStock.Contracts
{
    public class Product : IProduct
    {
        public string Label => throw new NotImplementedException();

        public decimal Price => throw new NotImplementedException();

        public int Quantity => throw new NotImplementedException();

        public int CompareTo([AllowNull] IProduct other)
        {
            throw new NotImplementedException();
        }
    }
}
