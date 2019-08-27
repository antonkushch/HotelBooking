using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BLL.DTO
{
    public class RoomDTO
    {
        public RoomCategoryDTO RoomCategory { get; set; }
        public int RoomNumber { get; set; }
        public decimal Price { get; set; }
    }
}
