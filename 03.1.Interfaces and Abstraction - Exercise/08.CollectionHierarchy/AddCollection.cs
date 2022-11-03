using System.Collections.Generic;

namespace CollectionHierarchy
{
    public class AddCollection : IAddRemoveCollection
    {
        public AddCollection()
        {
            Collection = new List<string>(100);
        }

        public List<string> Collection { get; private set; }

        public int Add(string item)
        {
            Collection.Add(item);
            return Collection.Count - 1;
        }

        public string Remove()
        {
            throw new System.NotImplementedException();
        }
    }
}
