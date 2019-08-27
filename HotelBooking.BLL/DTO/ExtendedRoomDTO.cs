using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BLL.DTO
{
    public class ExtendedRoomDTO
    {
        public int HotelID { get; set; }
        public int RoomNumber { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int PlacesInRoom { get; set; }
    }
}
