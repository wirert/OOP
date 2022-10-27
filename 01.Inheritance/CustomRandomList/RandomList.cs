using System;
using System.Collections.Generic;

namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        public string RandomString()
        {
            Random random = new Random();

            int index = random.Next(0, this.Count);
            string removedElement = this[index];
            this.RemoveAt(index);

            return removedElement;
        }
    }
}
