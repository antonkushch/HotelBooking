using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BLL.DTO
{
    public class ViewRelatedClientsDTO
    {
        public HotelDTO Hotel { get; set; }
        public IEnumerable<ClientDTO> Clients { get; set; }
    }
}
