using System.Collections.Generic;
using System.Linq;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty() => this.Any();

        public Stack<string> AddRange(string[] elementsToAdd)
        {
            foreach (var text in elementsToAdd)
            {
                Push(text);
            }

            return this;
        }
    }
}
