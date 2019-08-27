using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.DAL.Entities;
using HotelBooking.DAL.EF;
using HotelBooking.DAL.Interfaces;

namespace HotelBooking.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private HotelBookingContext db;
        private HotelRepository hotelRepository;
        private ClientRepository clientRepository;
        private BookingRepository bookingRepository;
        private HotelRoomMapRepository hotelRoomMapRepository;
        private RoomCategoryRepository roomCategoryRepository;
        private PriceOfHotelCategoryRepository priceOfHotelCategoryRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new HotelBookingContext(connectionString);
        }

        public IRepository<Hotel> Hotels
        {
            get
            {
                if (hotelRepository == null)
                    hotelRepository = new HotelRepository(db);
                return hotelRepository;
            }
        }

        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(db);
                return clientRepository;
            }
        }

        public IRepository<Booking> Bookings
        {
            get
            {
                if (bookingRepository == null)
                    bookingRepository = new BookingRepository(db);
                return bookingRepository;
            }
        }

        public IRepository<HotelRoomMap> HotelRoomMaps
        {
            get
            {
                if (hotelRoomMapRepository == null)
                    hotelRoomMapRepository = new HotelRoomMapRepository(db);
                return hotelRoomMapRepository;
            }
        }

        public IRepository<RoomCategory> RoomCategories
        {
            get
            {
                if (roomCategoryRepository == null)
                    roomCategoryRepository = new RoomCategoryRepository(db);
                return roomCategoryRepository;
            }
        }

        public IRepository<PriceOfHotelCategory> PricesOfHotelCategories
        {
            get
            {
                if (priceOfHotelCategoryRepository == null)
                    priceOfHotelCategoryRepository = new PriceOfHotelCategoryRepository(db);
                return priceOfHotelCategoryRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
