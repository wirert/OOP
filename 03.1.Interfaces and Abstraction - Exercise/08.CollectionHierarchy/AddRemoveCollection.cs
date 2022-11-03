using System.Collections.Generic;
using System.Linq;

namespace CollectionHierarchy
{
    public class AddRemoveCollection : IAddRemoveCollection
    {
        public AddRemoveCollection()
        {
            Collection = new List<string>();
        }

        public List<string> Collection { get; private set; }

        public int Add(string item)
        {
            Collection.Insert(0, item);
            return 0;
        }

        public virtual string Remove()
        {
            string item = Collection.Last();
            Collection.RemoveAt(Collection.Count - 1);

            return item;
        }
    }
}
