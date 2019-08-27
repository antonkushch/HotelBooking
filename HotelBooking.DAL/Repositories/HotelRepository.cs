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
    public class HotelRepository : IRepository<Hotel>
    {
        private HotelBookingContext db;

        public HotelRepository(HotelBookingContext context)
        {
            db = context;
        }

        public IEnumerable<Hotel> GetAll()
        {
            return db.Hotels;
        }

        public Hotel Get(int id)
        {
            return db.Hotels.Find(id);
        }

        public void Create(Hotel hotel)
        {
            db.Hotels.Add(hotel);
        }

        public void Update(Hotel hotel)
        {
            db.Entry(hotel).State = EntityState.Modified;
        }

        public IEnumerable<Hotel> Find(Func<Hotel, Boolean> predicate)
        {
            return db.Hotels.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            if (hotel != null)
                db.Hotels.Remove(hotel);
        }

        public void DeleteRange(IEnumerable<Hotel> range)
        {
            if (range != null)
                db.Hotels.RemoveRange(range);
        }

        public IQueryable<Hotel> GetAllQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
