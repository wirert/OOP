namespace BookingApp.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Models.Bookings.Contracts;
    using Repositories.Contracts;

    public class BookingRepository : IRepository<IBooking>
    {
        private readonly ICollection<IBooking> bookings;

        public BookingRepository()
        {
            bookings= new List<IBooking>();
        }

        public void AddNew(IBooking booking) => bookings.Add(booking);


        public IReadOnlyCollection<IBooking> All() => (IReadOnlyCollection<IBooking>)bookings;
        
        public IBooking Select(string bookingNumberAsString)
            => bookings.FirstOrDefault(b => b.BookingNumber.ToString() == bookingNumberAsString);        
    }
}
