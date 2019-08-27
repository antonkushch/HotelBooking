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
    public class ClientRepository : IRepository<Client>
    {
        private HotelBookingContext db;

        public ClientRepository(HotelBookingContext context)
        {
            db = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return db.Clients;
        }

        public Client Get(int id)
        {
            return db.Clients.Find(id);
        }

        public void Create(Client client)
        {
            db.Clients.Add(client);
        }

        public void Update(Client client)
        {
            db.Entry(client).State = EntityState.Modified;
        }

        public IEnumerable<Client> Find(Func<Client, Boolean> predicate)
        {
            return db.Clients.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Client client = db.Clients.Find(id);
            if (client != null)
                db.Clients.Remove(client);
        }

        public void DeleteRange(IEnumerable<Client> range)
        {
            if (range != null)
                db.Clients.RemoveRange(range);
        }

        public IQueryable<Client> GetAllQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
