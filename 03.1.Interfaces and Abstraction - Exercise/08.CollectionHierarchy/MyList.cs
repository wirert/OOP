using System.Linq;

namespace CollectionHierarchy
{
    public class MyList : AddRemoveCollection, IMyList
    {
        public int Used => Collection.Count;

        public override string Remove()
        {
            string item = Collection.First();
            Collection.RemoveAt(0);

            return item;
        }
    }
}
