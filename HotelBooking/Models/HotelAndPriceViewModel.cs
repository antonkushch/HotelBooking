using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HotelBooking.Models
{
    public class HotelAndPriceViewModel
    {
        public HotelAndPriceViewModel()
        {
            HotelDetails = new List<AddHotelHelpViewModel>();
        }

        public int HotelID { get; set; }
        [Required]
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
        //[DisplayName("Price of one night in hotel")]
        ////[Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Price must be positive and not 0.")]
        //public decimal PriceOfNight { get; set; }

        public List<AddHotelHelpViewModel> HotelDetails { get; set; }
    }
}