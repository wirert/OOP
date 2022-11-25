namespace BookingApp.Models.Hotels.Contacts
{
    using Bookings.Contracts;
    using Repositories.Contracts;
    using Rooms.Contracts;

    public interface IHotel
    {
        string FullName { get; }
        int Category { get; }
        double Turnover { get; }

        public IRepository<IRoom> Rooms { get; }
        public IRepository<IBooking> Bookings { get; }
    }
}
