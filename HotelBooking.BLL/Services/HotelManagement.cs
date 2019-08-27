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
using HotelBooking.DAL.Repositories;
using AutoMapper;

namespace HotelBooking.BLL.Services
{
    public class HotelManagement : IHotelManagement
    {
        IUnitOfWork Database { get; set; }

        public HotelManagement(IUnitOfWork unit)
        {
            Database = unit;
        }

        private void AddNewPriceForCategory(int? hotelID, int? categID, decimal price)
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

        private int AddNewHotelRoomMap(int? hotelID, int? categID, int roomsNum, int startWith)
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

        public void AddHotel(HotelAndPriceDTO hotelDto)
        {
            Hotel existingHotel = Database.Hotels.Find(x => x.Name == hotelDto.Name && x.Country == hotelDto.Country && x.City == hotelDto.City).FirstOrDefault();
            if (existingHotel != null)
                throw new ValidationException("This hotel already exists", "");

            int roomQuantity = 0;
            foreach (var item in hotelDto.HotelDetails)
                roomQuantity += item.NumOfRooms;

            if (roomQuantity != hotelDto.RoomQuantity)
                throw new ValidationException("Overall qty of rooms != sum of rooms of categories", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HotelAndPriceDTO, Hotel>()).CreateMapper();
            Hotel hotel = mapper.Map<HotelAndPriceDTO, Hotel>(hotelDto);
            
            Database.Hotels.Create(hotel);
            Database.Save();

            int newHotelID = hotel.HotelID;

            int startWith = 0;
            foreach (var elem in hotelDto.HotelDetails)
            {
                if (elem.NumOfRooms == 0)
                    continue;
                else
                {
                    AddNewPriceForCategory(newHotelID, elem.RoomCategoryID, elem.Price);
                    startWith = AddNewHotelRoomMap(newHotelID, elem.RoomCategoryID, elem.NumOfRooms, startWith);
                }
            }
        }

        public void DeleteHotel(int hotelID)
        {
            var hotelRoomMaps = Database.HotelRoomMaps.Find(x => x.HotelID == hotelID);
            if(hotelRoomMaps != null)
                Database.HotelRoomMaps.DeleteRange(hotelRoomMaps);

            var pricesOfCategories = Database.PricesOfHotelCategories.Find(x => x.HotelID == hotelID);
            if (pricesOfCategories != null)
                Database.PricesOfHotelCategories.DeleteRange(pricesOfCategories);

            Database.Hotels.Delete(hotelID);
            Database.Save();
        }

        public HotelDTO GetHotel(int? id)
        {
            if (id == null)
                throw new ValidationException("ID of hotel is not set", "");
            var hotel = Database.Hotels.Get(id.Value);
            if (hotel == null)
                throw new ValidationException("Hotel was not found", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, HotelDTO>()).CreateMapper();
            return mapper.Map<Hotel, HotelDTO>(hotel);
        }

        public IEnumerable<HotelDTO> GetHotels()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, HotelDTO>()).CreateMapper();
            var hotels =  mapper.Map<IEnumerable<Hotel>, List<HotelDTO>>(Database.Hotels.GetAll());
            return hotels;
        }

        public ViewHotelDTO ViewHotel(int? id)
        {
            if (id == null)
                throw new ValidationException("ID of hotel is not set", "");
            var hotel = Database.Hotels.Get(id.Value);
            if (hotel == null)
                throw new ValidationException("Hotel was not found", "");

            var hotelRoomMaps = Database.HotelRoomMaps.Find(x => x.HotelID == id).ToList();
            var roomCategories = Database.RoomCategories.GetAll().ToList();
            var prices = Database.PricesOfHotelCategories.Find(x => x.HotelID == id).ToList();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomCategory, RoomCategoryDTO>()).CreateMapper();
            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, HotelDTO>()).CreateMapper();

            var details = from hrMap in hotelRoomMaps
                          join roomCateg in roomCategories on hrMap.RoomCategoryID equals roomCateg.RoomCategoryID
                          join price in prices on roomCateg.RoomCategoryID equals price.RoomCategoryID
                          group hrMap by new { hrMap.RoomCategory, price.PriceForNight } into grp
                          select new ViewHotelRoomPricesDTO()
                          {
                              RoomCategory = mapper.Map<RoomCategory, RoomCategoryDTO>(grp.Key.RoomCategory),
                              NumOfRooms = grp.Count(),
                              Price = grp.Key.PriceForNight
                          };

            var returnObj = new ViewHotelDTO
            {
                Hotel = mapper.Map<Hotel, HotelDTO>(hotel),
                Details = details.ToList()
            };

            return returnObj;
        }

        public IEnumerable<HotelDTO> GetHotelsByName(string name)
        {
            if (name.Trim() == "")
                throw new ValidationException("Set the input textbox.", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, HotelDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Hotel>, List<HotelDTO>>(Database.Hotels.Find(x => x.Name.ToLower().Contains(name.ToLower())));
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        
    }
}
