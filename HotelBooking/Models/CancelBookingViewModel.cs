using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class CancelBookingViewModel
    {
        private DateTime defStartDate = new DateTime(2018, 5, 21);
        private DateTime defEndDate = new DateTime(2018, 5, 24);
        private DateTime defBirthDate = new DateTime(1998, 09, 10);

        public int HotelID { get; set; }
        public string HotelName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime BirthDate
        { get { return defBirthDate; } set { defBirthDate = value; } }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Room number must be positive and not 0.")]
        public int RoomNumber { get; set; }
        [Required]
        public DateTime StartDate { get { return defStartDate; } set { defStartDate = value; } }
        [Required]
        public DateTime EndDate { get { return defEndDate; } set { defEndDate = value; } }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PeopleQuantity must be positive and not 0.")]
        public int PeopleQuantity { get; set; }
    }
}