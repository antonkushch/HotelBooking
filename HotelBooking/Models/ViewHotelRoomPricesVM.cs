using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class ViewHotelRoomPricesVM
    {
        public RoomCategoryViewModel RoomCategory { get; set; }
        public int NumOfRooms { get; set; }
        public decimal Price { get; set; }
    }
}