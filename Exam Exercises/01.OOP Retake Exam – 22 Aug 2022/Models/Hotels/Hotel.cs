namespace BookingApp.Models.Hotels
{
    using System;
    using System.Collections.Generic;
    using BookingApp.Repositories;
    using Bookings.Contracts;
    using Contacts;
    using Repositories.Contracts;
    using Rooms.Contracts;
    using Utilities.Messages;

    public class Hotel : IHotel
    {
        private string fullName;
        private int category;
        private IRepository<IBooking> bookings;
        private IRepository<IRoom> rooms;

        private Hotel()
        {
            bookings = new BookingRepository();
            rooms = new RoomRepository();
        }

        public Hotel(string fullName, int category) : this() 
        {
            FullName = fullName;
            Category = category;
        }

        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HotelNameNullOrEmpty);
                }

                fullName = value;
            }
        }

        public int Category
        {
            get => this.category;
            private set
            {
                if (value < 1 || value > 5) 
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCategory);
                }

                category = value;
            }
        }

        public double Turnover
        {
            get
            {
                double sum = 0;

                foreach (var booking in Bookings.All())
                {
                    sum += booking.ResidenceDuration * booking.Room.PricePerNight;
                }

                return Math.Round(sum, 2);
            }
        }

        public IRepository<IRoom> Rooms => this.rooms;

        public IRepository<IBooking> Bookings => this.bookings;
    }
}
