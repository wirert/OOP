namespace BookingApp.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Models.Hotels.Contacts;
    using Repositories.Contracts;

    public class HotelRepository : IRepository<IHotel>
    {
        private readonly ICollection<IHotel> hotels;

        public HotelRepository()
        {
            hotels = new List<IHotel>();
        }

        public void AddNew(IHotel hotel) => hotels.Add(hotel);

        public IReadOnlyCollection<IHotel> All() => (IReadOnlyCollection<IHotel>)hotels;        

        public IHotel Select(string hotelName)
            => hotels.FirstOrDefault(h => h.FullName == hotelName);
    }
}
