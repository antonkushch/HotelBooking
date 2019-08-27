using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BLL.DTO
{
    public class BookingDTO
    {
        public int BookingID { get; set; }
        public HotelDTO Hotel { get; set; }
        public int HotelID { get; set; }
        public int RoomNumber { get; set; }
        public RoomCategoryDTO RoomCategory { get; set; }
        public ClientDTO Client { get; set; }
        public int ClientID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PeopleQuantity { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
