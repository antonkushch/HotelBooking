using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BLL.DTO
{
    public class FullBookingDTO
    {
        public int HotelID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PeopleQuantity { get; set; }
        public int InputRoomQuantity { get; set; }
        public List<Tuple<int, int, decimal>> FreeRooms { get; set; }
    }
}
