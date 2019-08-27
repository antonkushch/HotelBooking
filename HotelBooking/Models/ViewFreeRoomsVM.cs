using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class ViewFreeRoomsVM
    {
        public HotelViewModel Hotel { get; set; }
        public int NumFreeRooms { get; set; }
        public int NumRooms { get; set; }
        public IEnumerable<RoomViewModel> FreeRooms { get; set; }
    }
}