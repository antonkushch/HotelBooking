using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BLL.DTO
{
    public class ViewFreeRoomsDTO
    {
        public HotelDTO Hotel { get; set; }
        public int NumFreeRooms { get; set; }
        public int NumRooms { get; set; }
        public IEnumerable<RoomDTO> FreeRooms { get; set; }
    }
}
