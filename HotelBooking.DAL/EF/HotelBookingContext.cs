using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HotelBooking.DAL.Entities;

namespace HotelBooking.DAL.EF
{
    public class HotelBookingContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<HotelRoomMap> HotelRoomMaps { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<PriceOfHotelCategory> PricesOfHotelCategories { get; set; }

        static HotelBookingContext()
        {
            Database.SetInitializer<HotelBookingContext>(new StoreDbInitializer());
        }

        public HotelBookingContext(string connectionString) : base(connectionString)
        { }

        public class StoreDbInitializer : DropCreateDatabaseAlways<HotelBookingContext>
        {
            protected override void Seed(HotelBookingContext db)
            {
                db.Hotels.Add(new Hotel { HotelID = 1, Name = "Sheraton", Country = "Ukraine", City = "Lviv", RoomQuantity = 5, Email = "ads@gmail.com" });
                db.Hotels.Add(new Hotel { HotelID = 2, Name = "GoodJob ss", Country = "Spain", City = "Barcelona", RoomQuantity = 5, Email = "adswww@gmail.com" });
                db.Hotels.Add(new Hotel { HotelID = 3, Name = "Best Hotel", Country = "France", City = "Lyon", RoomQuantity = 4, Email = "adwe1s@gmail.com" });
                db.Hotels.Add(new Hotel { HotelID = 4, Name = "Western", Country = "Belgium", City = "Koorens", RoomQuantity = 5, Email = "ads1111@gmail.com" });
                db.Hotels.Add(new Hotel { HotelID = 5, Name = "United Walls", Country = "England", City = "Liverpool", RoomQuantity = 3, Email = "adssss@gmail.com" });

                db.Clients.Add(new Client { ClientID = 1, Name = "Adam", Surname = "Gnb", Phone = "+380959385940", BirthDate = new DateTime(1997, 2, 2) });
                db.Clients.Add(new Client { ClientID = 2, Name = "Brandon", Surname = "Cook", Phone = "+380952385940", BirthDate = new DateTime(1996, 3, 24) });
                db.Clients.Add(new Client { ClientID = 3, Name = "Kevin", Surname = "Ginger", Phone = "+380959385940", BirthDate = new DateTime(1997, 11, 12) });
                db.Clients.Add(new Client { ClientID = 4, Name = "Patrick", Surname = "Star", Phone = "+380959385940", BirthDate = new DateTime(1990, 10, 9) });
                db.Clients.Add(new Client { ClientID = 5, Name = "Adam", Surname = "Bartels", Phone = "+380959385940", BirthDate = new DateTime(1990, 10, 9) });
                db.Clients.Add(new Client { ClientID = 6, Name = "Yanick", Surname = "Yung", Phone = "+380959385940", BirthDate = new DateTime(1990, 10, 9) });
                db.Clients.Add(new Client { ClientID = 7, Name = "Kevin", Surname = "Adams", Phone = "+380959385940", BirthDate = new DateTime(1990, 10, 9) });

                db.Bookings.Add(new Booking { HotelID = 2, RoomNumber = 4, ClientID = 1, StartDate = new DateTime(2018, 6, 18), EndDate = new DateTime(2018, 6, 22), PeopleQuantity = 2, BookingDate = new DateTime(2018, 5, 10), TotalPrice = 1250 });
                db.Bookings.Add(new Booking { HotelID = 2, RoomNumber = 5, ClientID = 1, StartDate = new DateTime(2018, 6, 18), EndDate = new DateTime(2018, 6, 22), PeopleQuantity = 3, BookingDate = new DateTime(2018, 5, 11), TotalPrice = 1225 });
                db.Bookings.Add(new Booking { HotelID = 2, RoomNumber = 5, ClientID = 2, StartDate = new DateTime(2018, 6, 15), EndDate = new DateTime(2018, 6, 17), PeopleQuantity = 2, BookingDate = new DateTime(2018, 5, 12), TotalPrice = 750 });
                db.Bookings.Add(new Booking { HotelID = 1, RoomNumber = 1, ClientID = 3, StartDate = new DateTime(2018, 6, 13), EndDate = new DateTime(2018, 6, 15), PeopleQuantity = 1, BookingDate = new DateTime(2018, 5, 10), TotalPrice = 1200 });
                db.Bookings.Add(new Booking { HotelID = 3, RoomNumber = 1, ClientID = 4, StartDate = new DateTime(2018, 6, 13), EndDate = new DateTime(2018, 6, 15), PeopleQuantity = 2, BookingDate = new DateTime(2018, 5, 9), TotalPrice = 890 });
                db.Bookings.Add(new Booking { HotelID = 4, RoomNumber = 2, ClientID = 5, StartDate = new DateTime(2018, 6, 21), EndDate = new DateTime(2018, 6, 23), PeopleQuantity = 1, BookingDate = new DateTime(2018, 5, 19), TotalPrice = 690 });
                db.Bookings.Add(new Booking { HotelID = 5, RoomNumber = 2, ClientID = 6, StartDate = new DateTime(2018, 6, 19), EndDate = new DateTime(2018, 6, 25), PeopleQuantity = 3, BookingDate = new DateTime(2018, 5, 17), TotalPrice = 780 });

                db.RoomCategories.Add(new RoomCategory { RoomCategoryID = 1, Name = "1 Room apartment", PlacesQuantity = 1 });
                db.RoomCategories.Add(new RoomCategory { RoomCategoryID = 2, Name = "2 Room apartment", PlacesQuantity = 2 });
                db.RoomCategories.Add(new RoomCategory { RoomCategoryID = 3, Name = "4 Room apartment", PlacesQuantity = 4 });

                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 1, RoomCategoryID = 1, PriceForNight = 400 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 1, RoomCategoryID = 2, PriceForNight = 600 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 1, RoomCategoryID = 3, PriceForNight = 1000 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 2, RoomCategoryID = 1, PriceForNight = 350 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 2, RoomCategoryID = 2, PriceForNight = 500 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 2, RoomCategoryID = 3, PriceForNight = 850 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 3, RoomCategoryID = 1, PriceForNight = 850 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 3, RoomCategoryID = 2, PriceForNight = 1000 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 3, RoomCategoryID = 3, PriceForNight = 1325 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 4, RoomCategoryID = 1, PriceForNight = 640 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 4, RoomCategoryID = 2, PriceForNight = 820 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 4, RoomCategoryID = 3, PriceForNight = 1000 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 5, RoomCategoryID = 1, PriceForNight = 300 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 5, RoomCategoryID = 2, PriceForNight = 600 });
                db.PricesOfHotelCategories.Add(new PriceOfHotelCategory { HotelID = 5, RoomCategoryID = 3, PriceForNight = 1100 });

                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 1, RoomNumber = 1, RoomCategoryID = 1 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 1, RoomNumber = 2, RoomCategoryID = 1 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 1, RoomNumber = 3, RoomCategoryID = 3 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 1, RoomNumber = 4, RoomCategoryID = 1 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 1, RoomNumber = 5, RoomCategoryID = 2 });

                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 2, RoomNumber = 1, RoomCategoryID = 1 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 2, RoomNumber = 2, RoomCategoryID = 2 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 2, RoomNumber = 3, RoomCategoryID = 1 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 2, RoomNumber = 4, RoomCategoryID = 3 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 2, RoomNumber = 5, RoomCategoryID = 3 });

                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 3, RoomNumber = 1, RoomCategoryID = 1 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 3, RoomNumber = 2, RoomCategoryID = 1 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 3, RoomNumber = 3, RoomCategoryID = 1 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 3, RoomNumber = 4, RoomCategoryID = 2 });

                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 4, RoomNumber = 1, RoomCategoryID = 1 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 4, RoomNumber = 2, RoomCategoryID = 2 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 4, RoomNumber = 3, RoomCategoryID = 3 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 4, RoomNumber = 4, RoomCategoryID = 3 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 4, RoomNumber = 5, RoomCategoryID = 3 });

                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 5, RoomNumber = 1, RoomCategoryID = 1 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 5, RoomNumber = 2, RoomCategoryID = 1 });
                db.HotelRoomMaps.Add(new HotelRoomMap { HotelID = 5, RoomNumber = 3, RoomCategoryID = 2 });

                db.SaveChanges();
            }
        }
    }
}
