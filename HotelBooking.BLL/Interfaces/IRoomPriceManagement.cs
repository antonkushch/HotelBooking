using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.BLL.DTO;

namespace HotelBooking.BLL.Interfaces
{
    public interface IRoomPriceManagement
    {
        List<RoomCategoryDTO> GetRoomCategories();
        void AddNewPriceForCategory(int? hotelID, int? categID, decimal price);
        int AddNewHotelRoomMap(int? hotelID, int? categID, int roomsNum, int startWith);
        List<Tuple<int, int, decimal>> GetRoomsByHotelAndDate(int? hotelID, DateTime startDate, DateTime endDate);
        int GetRoomNumberForRoomCategory(int? hotelID, int inputRoomQuantity, DateTime startDate, DateTime endDate);
        void Dispose();
    }
}
