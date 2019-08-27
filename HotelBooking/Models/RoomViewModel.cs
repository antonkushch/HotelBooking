using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class RoomViewModel
    {
        public RoomCategoryViewModel RoomCategory { get; set; }
        public int RoomNumber { get; set; }
        public decimal Price { get; set; }
    }
}