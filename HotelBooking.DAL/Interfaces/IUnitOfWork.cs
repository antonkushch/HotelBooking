using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.DAL.Entities;

namespace HotelBooking.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Hotel> Hotels { get; }
        IRepository<Client> Clients { get; }
        IRepository<Booking> Bookings { get; }
        IRepository<HotelRoomMap> HotelRoomMaps { get; }
        IRepository<RoomCategory> RoomCategories { get; }
        IRepository<PriceOfHotelCategory> PricesOfHotelCategories { get; }

        void Save();
    }
}
