using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class ViewHotelViewModel
    {
        public HotelViewModel Hotel { get; set; }
        public List<ViewHotelRoomPricesVM> Details { get; set; }
    }
}