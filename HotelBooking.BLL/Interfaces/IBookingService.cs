using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.BLL.DTO;

namespace HotelBooking.BLL.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingDTO> GetBookings();
        IEnumerable<BookingDTO> GetBookingsByDate(DateTime? startDate, DateTime? endDate);
        BookingDTO GetBooking(int? id);
        void BookRoom(FullBookingDTO bookingDto);
        decimal CalculatePrice(DateTime startDate, DateTime endDate, int hotelID, int roomQuantity);
        void CancelBooking(BookingDTO bookingDto);
        ViewBookedRoomsDTO GetBookedRooms(int? hotelID);
        ViewFreeRoomsDTO GetFreeRooms(int? hotelID);
        void Dispose();
    }
}
