using System.Collections.Generic;

namespace CollectionHierarchy
{
    public interface IAddRemoveCollection
    {
        public List<string> Collection { get; }

        public int Add(string item);

        public string Remove();
    }
}
