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
    public class RoomCategoryRepository : IRepository<RoomCategory>
    {
        private HotelBookingContext db;

        public RoomCategoryRepository(HotelBookingContext context)
        {
            db = context;
        }

        public void Create(RoomCategory item)
        {
            db.RoomCategories.Add(item);
        }

        public void Delete(int id)
        {
            RoomCategory roomCategory = db.RoomCategories.Find(id);
            if (roomCategory != null)
                db.RoomCategories.Remove(roomCategory);
        }

        public void DeleteRange(IEnumerable<RoomCategory> range)
        {
            if (range != null)
                db.RoomCategories.RemoveRange(range);
        }

        public IEnumerable<RoomCategory> Find(Func<RoomCategory, bool> predicate)
        {
            return db.RoomCategories.Where(predicate).ToList();
        }

        public RoomCategory Get(int id)
        {
            return db.RoomCategories.Find(id);
        }

        public IEnumerable<RoomCategory> GetAll()
        {
            return db.RoomCategories;
        }

        public IQueryable<RoomCategory> GetAllQueryable()
        {
            throw new NotImplementedException();
        }

        public void Update(RoomCategory item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
