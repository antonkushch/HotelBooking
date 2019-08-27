using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.DAL.Entities
{
    public class Booking
    {
        public int BookingID { get; set; }

        public int HotelID { get; set; }
        public Hotel Hotel { get; set; }

        public int RoomNumber { get; set; }

        public int ClientID { get; set; }
        public Client Client { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime EndDate { get; set; }
        public int PeopleQuantity { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
