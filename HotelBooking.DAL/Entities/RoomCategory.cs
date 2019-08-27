using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Entities
{
    public class RoomCategory
    {
        public int RoomCategoryID { get; set; }
        public string Name { get; set; }
        public int PlacesQuantity { get; set; }
    }
}
