using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class ViewRelatedClientsVM
    {
        public HotelViewModel Hotel { get; set; }
        public IEnumerable<ClientViewModel> Clients { get; set; }
    }
}