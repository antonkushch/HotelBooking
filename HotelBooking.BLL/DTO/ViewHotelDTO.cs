using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BLL.DTO
{
    public class ViewHotelDTO
    {
        public HotelDTO Hotel { get; set; }
        public List<ViewHotelRoomPricesDTO> Details { get; set; }
    }
}
