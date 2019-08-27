using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.DAL.Entities
{
    public class Hotel
    {
        public int HotelID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int RoomQuantity { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }

        public ICollection<Booking> Bookings { get; set; }
        public ICollection<PriceOfHotelCategory> PricesOfHotelCategories { get; set; }
    }
}
