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
    public class HotelRoomMapRepository : IRepository<HotelRoomMap>
    {
        private HotelBookingContext db;

        public HotelRoomMapRepository(HotelBookingContext context)
        {
            db = context;
        }

        public void Create(HotelRoomMap item)
        {
            db.HotelRoomMaps.Add(item);
        }

        public void Delete(int id)
        {
            HotelRoomMap hotelRoomMap = db.HotelRoomMaps.Find(id);
            if (hotelRoomMap != null)
                db.HotelRoomMaps.Remove(hotelRoomMap);
        }

        public void DeleteRange(IEnumerable<HotelRoomMap> range)
        {
            if (range != null)
                db.HotelRoomMaps.RemoveRange(range);
        }

        public IEnumerable<HotelRoomMap> Find(Func<HotelRoomMap, bool> predicate)
        {
            return db.HotelRoomMaps.Where(predicate).ToList();
        }

        public void Update(HotelRoomMap item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<HotelRoomMap> GetAll()
        {
            return db.HotelRoomMaps.Include(x => x.RoomCategory);
        }

        public HotelRoomMap Get(int id)
        {
            return db.HotelRoomMaps.Find(id);
        }

        public IQueryable<HotelRoomMap> GetAllQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
