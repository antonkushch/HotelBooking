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
    public class PriceOfHotelCategoryRepository : IRepository<PriceOfHotelCategory>
    {
        private HotelBookingContext db;

        public PriceOfHotelCategoryRepository(HotelBookingContext context)
        {
            db = context;
        }

        public void Create(PriceOfHotelCategory item)
        {
            db.PricesOfHotelCategories.Add(item);
        }

        public void Delete(int id)
        {
            PriceOfHotelCategory priceOfCat = db.PricesOfHotelCategories.Find(id);
            if (priceOfCat != null)
                db.PricesOfHotelCategories.Remove(priceOfCat);
        }

        public void DeleteRange(IEnumerable<PriceOfHotelCategory> range)
        {
            if(range != null)
                db.PricesOfHotelCategories.RemoveRange(range);
        }

        public IEnumerable<PriceOfHotelCategory> Find(Func<PriceOfHotelCategory, bool> predicate)
        {
            return db.PricesOfHotelCategories.Where(predicate).ToList();
        }

        public PriceOfHotelCategory Get(int id)
        {
            return db.PricesOfHotelCategories.Find(id);
        }

        public IEnumerable<PriceOfHotelCategory> GetAll()
        {
            return db.PricesOfHotelCategories;
        }

        public IQueryable<PriceOfHotelCategory> GetAllQueryable()
        {
            throw new NotImplementedException();
        }

        public void Update(PriceOfHotelCategory item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }
}
