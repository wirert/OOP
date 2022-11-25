namespace BookingApp.Models.Rooms
{
    public class DoubleBed : Room
    {
        private const int Bed_Capacity = 2;

        public DoubleBed() : base(Bed_Capacity)
        {
        }
    }
}
