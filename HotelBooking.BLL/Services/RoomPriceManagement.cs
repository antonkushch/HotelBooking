using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.BLL.DTO;
using HotelBooking.BLL.Infrastructure;
using HotelBooking.BLL.Interfaces;
using HotelBooking.DAL.Entities;
using HotelBooking.DAL.Interfaces;
using AutoMapper;

namespace HotelBooking.BLL.Services
{
    public class RoomPriceManagement : IRoomPriceManagement
    {
        IUnitOfWork Database { get; set; }

        public RoomPriceManagement(IUnitOfWork unit)
        {
            Database = unit;
        }

        public List<RoomCategoryDTO> GetRoomCategories()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomCategory, RoomCategoryDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<RoomCategory>, List<RoomCategoryDTO>>(Database.RoomCategories.GetAll());
        }

        public void AddNewPriceForCategory(int? hotelID, int? categID, decimal price)
        {
            if (hotelID == null || categID == null)
                throw new ValidationException("ID of hotel or category is not set", "");

            PriceOfHotelCategory newPrices = new PriceOfHotelCategory
            {
                HotelID = hotelID.Value,
                RoomCategoryID = categID.Value,
                PriceForNight = price
            };

            Database.PricesOfHotelCategories.Create(newPrices);
            Database.Save();
        }

        public int AddNewHotelRoomMap(int? hotelID, int? categID, int roomsNum, int startWith)
        {
            //int _startWith = startWith + 1;
            int _endWhen = roomsNum + startWith;
            for (int i = startWith + 1; i <= _endWhen; i++)
            {
                HotelRoomMap map = new HotelRoomMap
                {
                    HotelID = hotelID.Value,
                    RoomNumber = i,
                    RoomCategoryID = categID.Value
                };
                Database.HotelRoomMaps.Create(map);
            }
            Database.Save();
            return _endWhen;
        }

        public List<Tuple<int, int, decimal>> GetRoomsByHotelAndDate(int? hotelID, DateTime startDate, DateTime endDate)
        {
            if (hotelID == null)
                throw new ValidationException("ID of hotel is not set.", "");

            if(startDate >= endDate)
                throw new ValidationException("Start date should be earlier then end date.", "");

            var prices = Database.PricesOfHotelCategories.Find(x => x.HotelID == hotelID).ToList();
            var bookings = Database.Bookings.GetAll().ToList();
            var hotelRoomMaps = Database.HotelRoomMaps.GetAll().ToList();

            //var query = hotelRoomMaps.Where(x => !bookings.Where(n => n.StartDate <= endDate && n.EndDate >= startDate && n.HotelID == hotelID)
            //                                .Select(y => y.RoomNumber).Contains(x.RoomNumber) && x.HotelID == hotelID).ToList();

            var query1 = from hrMap in hotelRoomMaps
                         join price in prices on hrMap.RoomCategoryID equals price.RoomCategoryID
                         where hrMap.HotelID == hotelID && !bookings.Where(n => n.StartDate <= endDate && n.EndDate >= startDate && n.HotelID == hotelID)
                                             .Select(y => y.RoomNumber).Contains(hrMap.RoomNumber)
                         select new
                         {
                             roomCateg = hrMap.RoomCategory,
                             price = price.PriceForNight
                         };

            var extDtoGrouped = query1.GroupBy(i => new { i.roomCateg.RoomCategoryID, i.roomCateg.PlacesQuantity, i.price })
                                        .Select(grp => new Tuple<int, int, decimal>(grp.Key.PlacesQuantity, grp.Count(), grp.Key.price)).ToList();

            return extDtoGrouped;
        }

        public int GetRoomNumberForRoomCategory(int? hotelID, int inputRoomQuantity, DateTime startDate, DateTime endDate)
        {
            if (hotelID == null)
                throw new ValidationException("ID of hotel or category is not set", "");

            var roomCategories = Database.RoomCategories.GetAll().ToList();
            var hotelRoomMaps = Database.HotelRoomMaps.GetAll().ToList();
            var bookings = Database.Bookings.GetAll().ToList();

            var roomNum = (from roomCategory in roomCategories
                         join hrMap in hotelRoomMaps on roomCategory.RoomCategoryID equals hrMap.RoomCategoryID
                         where hrMap.HotelID == hotelID && roomCategory.PlacesQuantity == inputRoomQuantity && !bookings.Where(n => n.StartDate <= endDate && n.EndDate >= startDate && n.HotelID == hotelID)
                                            .Select(y => y.RoomNumber).Contains(hrMap.RoomNumber)
                           select hrMap.RoomNumber).FirstOrDefault();

            return roomNum;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
