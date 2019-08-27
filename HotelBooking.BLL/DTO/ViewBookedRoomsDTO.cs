using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BLL.DTO
{
    public class ViewBookedRoomsDTO
    {
        public HotelDTO Hotel { get; set; }
        public int NumBookedRooms { get; set; }
        public int NumRooms { get; set; }
        public IEnumerable<BookingDTO> BookedRooms { get; set; }
    }
}
