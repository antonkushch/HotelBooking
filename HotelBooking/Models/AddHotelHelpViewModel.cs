using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HotelBooking.Models
{
    public class AddHotelHelpViewModel
    {
        public int RoomCategoryID { get; set; }
        public string RoomCategoryName { get; set; }
        [DisplayName("Number of rooms of this type")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of rooms must be positive.")]
        public int NumOfRooms { get; set; }
        [DisplayName("Price of 1 night in room of this type")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be positive.")]
        public decimal Price { get; set; }
    }
}