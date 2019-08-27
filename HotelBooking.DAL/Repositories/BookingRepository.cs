using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.DAL.Entities;
using HotelBooking.DAL.EF;
using HotelBooking.DAL.Interfaces;

namespace HotelBooking.DAL.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        private HotelBookingContext db;

        public BookingRepository(HotelBookingContext context)
        {
            db = context;
        }

        public IEnumerable<Booking> GetAll()
        {
            return db.Bookings.Include(x => x.Client).Include(o => o.Hotel);
        }

        public IQueryable<Booking> GetAllQueryable()
        {
            return db.Bookings.Include(x => x.Client).Include(o => o.Hotel);
        }

        public Booking Get(int id)
        {
            return db.Bookings.Find(id);
        }

        public void Create(Booking booking)
        {
            db.Bookings.Add(booking);
        }

        public void Update(Booking booking)
        {
            db.Entry(booking).State = EntityState.Modified;
        }

        public IEnumerable<Booking> Find(Func<Booking, Boolean> predicate)
        {
            return db.Bookings.Include(x => x.Client).Include(o => o.Hotel).Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking != null)
                db.Bookings.Remove(booking);
        }

        public void DeleteRange(IEnumerable<Booking> range)
        {
            if (range != null)
                db.Bookings.RemoveRange(range);
        }
    }
}
