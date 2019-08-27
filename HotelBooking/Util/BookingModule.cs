using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using HotelBooking.BLL.Services;
using HotelBooking.BLL.Interfaces;

namespace HotelBooking.Util
{
    public class BookingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookingService>().To<BookingService>();
            Bind<IClientManagement>().To<ClientManagement>();
            Bind<IHotelManagement>().To<HotelManagement>();
            Bind<IRoomPriceManagement>().To<RoomPriceManagement>();
        }
    }
}