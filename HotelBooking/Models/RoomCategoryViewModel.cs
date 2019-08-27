using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class RoomCategoryViewModel
    {
        public int RoomCategoryID { get; set; }
        [DisplayName("Room category")]
        public string Name { get; set; }
        public int PlacesQuantity { get; set; }
    }
}