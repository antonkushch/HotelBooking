using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Entities
{
    public class PriceOfHotelCategory
    {
        public int PriceOfHotelCategoryID { get; set; }

        public int HotelID { get; set; }
        public Hotel Hotel { get; set; }

        public int RoomCategoryID { get; set; }
        public RoomCategory RoomCategory { get; set; }

        public decimal PriceForNight { get; set; }
    }
}
