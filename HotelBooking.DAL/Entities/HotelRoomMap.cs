using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Entities
{
    public class HotelRoomMap
    {
        public int HotelRoomMapID { get; set; }

        public int HotelID { get; set; }
        public Hotel Hotel { get; set; }

        public int RoomNumber { get; set; }

        public int RoomCategoryID { get; set; }
        public RoomCategory RoomCategory { get; set; }
    }
}
