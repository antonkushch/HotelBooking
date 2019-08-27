using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HotelBooking.Models
{
    public class FullBookingViewModel
    {
        private DateTime defStartDate = new DateTime(2018, 6, 20);
        private DateTime defEndDate = new DateTime(2018, 6, 23);
        private DateTime defBirthDate = new DateTime(1998, 9, 10);

        public int HotelID { get; set; }
        public string HotelName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime BirthDate
        { get { return defBirthDate; } set { defBirthDate = value; } }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        public DateTime StartDate { get { return defStartDate; } set { defStartDate = value; } }
        [Required]
        public DateTime EndDate { get { return defEndDate; } set { defEndDate = value; } }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PeopleQuantity must be positive and not 0.")]
        public int PeopleQuantity { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Room qunatity must be positive and not 0.")]
        public int InputRoomQuantity { get; set; }

        public List<Tuple<int, int, decimal>> FreeRooms { get; set; }
    }
}