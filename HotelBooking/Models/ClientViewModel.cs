using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class ClientViewModel
    {
        //private DateTime defBirthDate = new DateTime(1998, 09, 10);

        public int ClientID { get; set; }
        [Required]
        [DisplayName("Client Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Client Surname")]
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
    }
}