using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelBooking.BLL.DTO;

namespace HotelBooking.Models
{
    public class BookingViewModel
    {
        public int BookingID { get; set; }
        public HotelViewModel Hotel { get; set; }
        public int HotelID { get; set; }
        public int RoomNumber { get; set; }
        public RoomCategoryViewModel RoomCategory { get; set; }
        public ClientViewModel Client { get; set; }
        public int ClientID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PeopleQuantity { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}