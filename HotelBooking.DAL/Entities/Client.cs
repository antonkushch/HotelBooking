using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.DAL.Entities
{
    public class Client
    {
        public int ClientID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime BirthDate { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
