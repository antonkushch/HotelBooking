using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BLL.DTO
{
    public class HotelAndPriceDTO
    {
        public int HotelID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int RoomQuantity { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public List<AddHotelHelpDTO> HotelDetails { get; set; }
    }
}
