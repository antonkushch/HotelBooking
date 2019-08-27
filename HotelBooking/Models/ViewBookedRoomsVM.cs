using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class ViewBookedRoomsVM
    {
        public HotelViewModel Hotel { get; set; }
        public int NumBookedRooms { get; set; }
        public int NumRooms { get; set; }
        public IEnumerable<BookingViewModel> BookedRooms { get; set; }
    }
}