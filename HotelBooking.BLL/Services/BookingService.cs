using System;
using System.Collections;
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
    public class BookingService : IBookingService
    {
        IUnitOfWork Database { get; set; }

        public BookingService(IUnitOfWork unit)
        {
            Database = unit;
        }

        public IEnumerable<BookingDTO> GetBookings()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingDTO>()).CreateMapper();
            return mapper.Map<IQueryable<Booking>, List<BookingDTO>>(Database.Bookings.GetAllQueryable());
        }

        public BookingDTO GetBooking(int? id)
        {
            if(id == null)
                throw new ValidationException("ID of booking is not set.", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingDTO>()).CreateMapper();

            var booking = Database.Bookings.Find(x => x.BookingID == id.Value).SingleOrDefault();
            var hotelRoomMap = Database.HotelRoomMaps.Find(x => x.HotelID == booking.Hotel.HotelID && x.RoomNumber == booking.RoomNumber).SingleOrDefault();
            var roomCateg = Database.RoomCategories.Get(hotelRoomMap.RoomCategoryID);

            return new BookingDTO { BookingID = booking.BookingID, Hotel = mapper.Map<Hotel, HotelDTO>(booking.Hotel), RoomNumber = booking.RoomNumber, Client = mapper.Map<Client, ClientDTO>(booking.Client), StartDate = booking.StartDate, EndDate = booking.EndDate, PeopleQuantity = booking.PeopleQuantity, BookingDate = booking.BookingDate, TotalPrice = booking.TotalPrice, RoomCategory = mapper.Map<RoomCategory, RoomCategoryDTO>(roomCateg) };
        }
        
        public IEnumerable<BookingDTO> GetBookingsByDate(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null || startDate >= endDate)
                throw new ValidationException("Properly set the dates.", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Booking>, List<BookingDTO>>(Database.Bookings.Find(x => x.StartDate <= endDate && x.EndDate >= startDate));
        }

        private Client AddClient(ClientDTO clientDto)
        {
            bool existingClient = false;
            Client cl = Database.Clients.Find(x => x.Name == clientDto.Name && x.Surname == clientDto.Surname && x.BirthDate == clientDto.BirthDate).FirstOrDefault();
            if (cl != null)
                existingClient = true;

            if (!existingClient)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, Client>()).CreateMapper();
                Client client = mapper.Map<ClientDTO, Client>(clientDto);

                Database.Clients.Create(client);
                Database.Save();
                return client;
            }

            return cl;
        }

        private int GetRoomNumberForRoomCategory(int? hotelID, int inputRoomQuantity, DateTime startDate, DateTime endDate)
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

        public void BookRoom(FullBookingDTO bookingDto)
        {
            Hotel hotel = Database.Hotels.Get(bookingDto.HotelID);

            if (hotel == null)
                throw new ValidationException("This hotel does not exist", "");

            CheckRoomAvailability(bookingDto.InputRoomQuantity, bookingDto.HotelID, bookingDto.StartDate, bookingDto.EndDate, bookingDto.FreeRooms);

            ClientDTO client = new ClientDTO { Name = bookingDto.Name, Surname = bookingDto.Surname, BirthDate = bookingDto.BirthDate, Phone = bookingDto.Phone, Email = bookingDto.Email };
            Client newClient = AddClient(client);

            decimal totalPrice = CalculatePrice(bookingDto.StartDate, bookingDto.EndDate, bookingDto.HotelID, bookingDto.InputRoomQuantity);
            int roomNumber = GetRoomNumberForRoomCategory(bookingDto.HotelID, bookingDto.InputRoomQuantity, bookingDto.StartDate, bookingDto.EndDate);

            Booking booking = new Booking
            {
                Hotel = hotel,
                RoomNumber = roomNumber,
                Client = newClient,
                StartDate = bookingDto.StartDate,
                EndDate = bookingDto.EndDate,
                PeopleQuantity = bookingDto.PeopleQuantity,
                BookingDate = DateTime.Now,
                TotalPrice = totalPrice
            };

            Database.Bookings.Create(booking);
            Database.Save();
        }

        public void CancelBooking(BookingDTO bookingDto)
        {
            if(bookingDto.StartDate >= bookingDto.EndDate)
                throw new ValidationException("Start date should be earlier then end date.", "");

            Hotel hotel = Database.Hotels.Get(bookingDto.HotelID);

            if (hotel == null)
                throw new ValidationException("This hotel does not exist", "");

            Booking booking = Database.Bookings.Find(x => x.ClientID == bookingDto.ClientID && x.RoomNumber == bookingDto.RoomNumber && x.HotelID == bookingDto.HotelID && x.StartDate == bookingDto.StartDate && x.EndDate == bookingDto.EndDate && x.PeopleQuantity == bookingDto.PeopleQuantity).FirstOrDefault();
            if(booking == null)
                throw new ValidationException("This booking was not found", "");

            Database.Bookings.Delete(booking.BookingID);
            Database.Save();
        }

        private bool CheckRoomAvailability(int roomCategory, int? hotelID, DateTime startDate, DateTime endDate, List<Tuple<int, int, decimal>> freeRooms)
        {
            if (hotelID == null)
                throw new ValidationException("ID of hotel is not set", "");
            //int roomQuantity;
            bool isInList = false;

            foreach(var item in freeRooms)
            {
                if(item.Item1 == roomCategory)
                {
                    if (item.Item2 > 0)
                        isInList = true;
                }
            }

            if (isInList == false)
                throw new RoomAlreadyBookedException("This category is not available. See available categories.", "");

            return isInList;
        }

        public decimal CalculatePrice(DateTime startDate, DateTime endDate, int hotelID, int roomQuantity)
        {
            int days = (endDate.Date - startDate.Date).Days;
            var roomCatID = Database.RoomCategories.Find(x => x.PlacesQuantity == roomQuantity).Select(x => x.RoomCategoryID).FirstOrDefault();
            var priceObject = Database.PricesOfHotelCategories.Find(x => x.HotelID == hotelID && x.RoomCategoryID == roomCatID).FirstOrDefault();

            if (priceObject != null)
                return priceObject.PriceForNight * days;
            else
                throw new ValidationException("The price for this category was not found.", "");  
        }

        private int GetNumOfBookedRooms(int hotelID)
        {
            var num = Database.Bookings.Find(x => x.HotelID == hotelID).Select(x => x.RoomNumber).Distinct().Count();
            return num;
        }

        private int GetNumOfFreeRooms(Hotel hotel)
        {
            var numOfBookedRooms = GetNumOfBookedRooms(hotel.HotelID);
            return hotel.RoomQuantity - numOfBookedRooms;
        }

        public ViewBookedRoomsDTO GetBookedRooms(int? hotelID)
        {
            if (hotelID == null)
                throw new ValidationException("ID of hotel is not set", "");

            Hotel hotel = Database.Hotels.Get(hotelID.Value);
            if(hotel == null)
                throw new ValidationException("Hotel was not found.", "");

            var numOfBookedRooms = GetNumOfBookedRooms(hotel.HotelID);

            var bookings = Database.Bookings.Find(x => x.HotelID == hotelID).ToList();
            var hotelRoomMaps = Database.HotelRoomMaps.Find(x => x.HotelID == hotelID).ToList();
            var roomCategories = Database.RoomCategories.GetAll().ToList();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<RoomCategory, RoomCategoryDTO>()).CreateMapper();
            var mapper3 = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, HotelDTO>()).CreateMapper();

            var query = from booking in bookings
                        join hrMap in hotelRoomMaps on booking.RoomNumber equals hrMap.RoomNumber
                        join roomCateg in roomCategories on hrMap.RoomCategoryID equals roomCateg.RoomCategoryID
                        select new BookingDTO()
                        {
                            BookingID = booking.BookingID,
                            Client = mapper.Map<Client, ClientDTO>(booking.Client),
                            RoomCategory = mapper.Map<RoomCategory, RoomCategoryDTO>(roomCateg),
                            RoomNumber = booking.RoomNumber,
                            StartDate = booking.StartDate,
                            EndDate = booking.EndDate,
                            TotalPrice = booking.TotalPrice
                        };

            var returnObj = new ViewBookedRoomsDTO
            {
                Hotel = mapper.Map<Hotel, HotelDTO>(hotel),
                NumBookedRooms = numOfBookedRooms,
                NumRooms = hotel.RoomQuantity,
                BookedRooms = query
            };

            return returnObj;
        }

        public ViewFreeRoomsDTO GetFreeRooms(int? hotelID)
        {
            if (hotelID == null)
                throw new ValidationException("ID of hotel is not set", "");

            Hotel hotel = Database.Hotels.Get(hotelID.Value);
            if (hotel == null)
                throw new ValidationException("Hotel was not found.", "");

            var numOfFreeRooms = GetNumOfFreeRooms(hotel);

            var bookings = Database.Bookings.Find(x => x.HotelID == hotelID).ToList();
            var hotelRoomMaps = Database.HotelRoomMaps.Find(x => x.HotelID == hotelID).ToList();
            var roomCategories = Database.RoomCategories.GetAll().ToList();
            var prices = Database.PricesOfHotelCategories.Find(x => x.HotelID == hotelID).ToList();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomCategory, RoomCategoryDTO>()).CreateMapper();
            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, HotelDTO>()).CreateMapper();

            var query = from hrMap in hotelRoomMaps
                        join roomCateg in roomCategories on hrMap.RoomCategoryID equals roomCateg.RoomCategoryID
                        join price in prices on roomCateg.RoomCategoryID equals price.RoomCategoryID
                        where !bookings.Where(n => n.HotelID == hotelID).Select(y => y.RoomNumber).Contains(hrMap.RoomNumber)
                        select new RoomDTO()
                        {
                            RoomCategory = mapper.Map<RoomCategory, RoomCategoryDTO>(roomCateg),
                            RoomNumber = hrMap.RoomNumber,
                            Price = price.PriceForNight
                        };

            var returnObj = new ViewFreeRoomsDTO
            {
                Hotel = mapper2.Map<Hotel, HotelDTO>(hotel),
                NumFreeRooms = numOfFreeRooms,
                NumRooms = hotel.RoomQuantity,
                FreeRooms = query
            };

            return returnObj;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
