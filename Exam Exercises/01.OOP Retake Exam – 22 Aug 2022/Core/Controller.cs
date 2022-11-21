namespace BookingApp.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Contracts;
    using Models.Bookings;
    using Models.Hotels;
    using Models.Hotels.Contacts;
    using Models.Rooms.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using Utilities.Messages;

    public class Controller : IController
    {
        private IRepository<IHotel> hotelRepository;

        public Controller()
        {
            hotelRepository = new HotelRepository();
        }

        public string AddHotel(string hotelName, int category)
        {
            if (hotelRepository.All().Any(h => h.FullName == hotelName))
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }

            Hotel hotel = new Hotel(hotelName, category);
            hotelRepository.AddNew(hotel);

            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            var hotelsWithGivenCategory = hotelRepository.All()
                .Where(h => h.Category == category)
                .OrderBy(h => h.FullName).ToArray();

            if (hotelsWithGivenCategory.Length == 0)
            {
                return string.Format(OutputMessages.CategoryInvalid, category);
            }

            IRoom roomToBook = null;

            foreach (var hotel in hotelsWithGivenCategory)
            {
                roomToBook = hotel.Rooms.All()
                    .Where(r => r.PricePerNight > 0)
                    .OrderBy(r => r.BedCapacity)
                    .FirstOrDefault(r => r.BedCapacity >= adults + children);

                if (roomToBook != null)
                {
                    int bookingNumber = hotel.Bookings.All().Count + 1;
                    hotel.Bookings.AddNew(new Booking(roomToBook, duration, adults, children, bookingNumber));

                    return string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotel.FullName);
                }
            }

            return OutputMessages.RoomNotAppropriate;
        }

        public string HotelReport(string hotelName)
        {
            var hotel = hotelRepository.All().FirstOrDefault(h => h.FullName == hotelName);

            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Hotel name: {hotelName}")
                .AppendLine($"--{hotel.Category} star hotel")
                .AppendLine($"--Turnover: {hotel.Turnover:F2} $")
                .AppendLine("--Bookings:")
                .AppendLine();

            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine("none");
            }

            foreach (var booking in hotel.Bookings.All())
            {
                sb.AppendLine(booking.BookingSummary())
                    .AppendLine();
            }

            return sb.ToString().Trim();
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            IHotel hotel = hotelRepository.All().FirstOrDefault(h => h.FullName.ToLower() == hotelName.ToLower());

            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            //this method will throw exeption if there is no such RoomType
            Type typeRoom = FindGivenTypeNameInAssembly(roomTypeName);
            IRoom room = hotel.Rooms.All().FirstOrDefault(r => r.GetType().Name == roomTypeName);

            if (room == null)
            {
                return OutputMessages.RoomTypeNotCreated;
            }

            if (room.PricePerNight > 0)
            {
                throw new InvalidOperationException(ExceptionMessages.CannotResetInitialPrice);
            }

            room.SetPrice(price);

            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            var hotel = hotelRepository.Select(hotelName);

            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            if (hotel.Rooms.All().Any(r => r.GetType().Name.ToLower() == roomTypeName.ToLower()))
            {
                return OutputMessages.RoomTypeAlreadyCreated;
            }

            IRoom roomToAdd = InstanceRoomByTypeName(roomTypeName);

            hotel.Rooms.AddNew(roomToAdd);

            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }

        private IRoom InstanceRoomByTypeName(string roomTypeName)
        {
            var roomTypeToInstance = FindGivenTypeNameInAssembly(roomTypeName);

            return (IRoom)Activator.CreateInstance(roomTypeToInstance);
        }

        private Type FindGivenTypeNameInAssembly(string typeName)
        {
           Assembly assembly = Assembly.GetCallingAssembly();

            Type type = assembly.GetTypes().FirstOrDefault(t => t.Name.ToLower() == typeName.ToLower());

            if (type == null)
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            return type;
        }
    }
}
