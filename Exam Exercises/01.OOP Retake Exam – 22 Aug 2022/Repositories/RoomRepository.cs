namespace BookingApp.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Models.Rooms.Contracts;
    using Repositories.Contracts;

    public class RoomRepository : IRepository<IRoom>
    {
        private readonly ICollection<IRoom> rooms;

        public RoomRepository()
        {
            rooms = new List<IRoom>();
        }

        public void AddNew(IRoom room) => rooms.Add(room);


        public IReadOnlyCollection<IRoom> All() => rooms as IReadOnlyCollection<IRoom>;
        

        public IRoom Select(string roomTypeName)
            => rooms.First(r => r.GetType().Name.ToLower() == roomTypeName.ToLower());       
    }
}
