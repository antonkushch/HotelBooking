using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.BLL.DTO;

namespace HotelBooking.BLL.Interfaces
{
    public interface IHotelManagement
    {
        void AddHotel(HotelAndPriceDTO hotelDto);
        void DeleteHotel(int hotelID);
        IEnumerable<HotelDTO> GetHotels();
        HotelDTO GetHotel(int? id);
        ViewHotelDTO ViewHotel(int? id);
        IEnumerable<HotelDTO> GetHotelsByName(string name);
        void Dispose();
    }
}
