using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class HotelViewModel
    {
        public int HotelID { get; set; }
        [Required]
        [DisplayName("Hotel")]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "RoomQuantity must be positive and not 0.")]
        public int RoomQuantity { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}