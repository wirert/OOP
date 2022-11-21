namespace BookingApp.Models.Rooms
{    
    public class Apartment : Room
    {
        private const int Bed_Capacity = 6;

        public Apartment() : base(Bed_Capacity)
        {
        }
    }
}
