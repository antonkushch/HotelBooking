using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class ClientListVM
    {
        public IList<ClientViewModel> Clients { get; set; }
    }
}