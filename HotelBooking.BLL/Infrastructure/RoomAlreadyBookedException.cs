using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BLL.Infrastructure
{
    public class RoomAlreadyBookedException : Exception
    {
        public string Property { get; protected set; }
        public RoomAlreadyBookedException(string msg, string prop) : base(msg)
        {
            Property = prop;
        }
    }
}
