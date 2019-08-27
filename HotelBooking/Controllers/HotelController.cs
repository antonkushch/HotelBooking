using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HotelBooking.BLL.Interfaces;
using HotelBooking.BLL.DTO;
using HotelBooking.BLL.Infrastructure;
using HotelBooking.Models;

namespace HotelBooking.Controllers
{
    public class HotelController : Controller
    {
        IHotelManagement hotelService;
        IRoomPriceManagement roomPriceService;
        IClientManagement clientService;
        IBookingService bookingService;

        public HotelController(IHotelManagement service, IRoomPriceManagement service2, IClientManagement service3, IBookingService service4)
        {
            hotelService = service;
            roomPriceService = service2;
            clientService = service3;
            bookingService = service4;
        }

        public ActionResult Index()
        {
            IEnumerable<HotelDTO> hotelDtos = hotelService.GetHotels();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HotelDTO, HotelViewModel>()).CreateMapper();
            var hotels = mapper.Map<IEnumerable<HotelDTO>, List<HotelViewModel>>(hotelDtos);
            return View(hotels);
        }

        [HttpPost]
        public ActionResult Index(IList<HotelViewModel> hotels, string hotelName)
        {
            Session["hotelName"] = hotelName;
            IEnumerable<HotelDTO> hotelDtos;
            try
            {
                hotelDtos = hotelService.GetHotelsByName(hotelName);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HotelDTO, HotelViewModel>()).CreateMapper();
                var resultHotels = mapper.Map<IEnumerable<HotelDTO>, IList<HotelViewModel>>(hotelDtos);
                return View("Index", resultHotels);
            }
            catch (ValidationException ex)
            {
                ViewBag.Error = ex.Message;
                if (hotels == null)
                    return View("Index", new List<HotelViewModel>());
                return View("Index", hotels);
            }
        }

        [HttpGet]
        public ActionResult AddHotel()
        {
            var categories = roomPriceService.GetRoomCategories();
            List<AddHotelHelpViewModel> helperList = new List<AddHotelHelpViewModel>();

            foreach (var item in categories)
            {
                helperList.Add(new AddHotelHelpViewModel { RoomCategoryID = item.RoomCategoryID, RoomCategoryName = item.Name });
            }

            var returnView = new HotelAndPriceViewModel { HotelDetails = helperList };

            return View(returnView);
        }

        [HttpPost]
        public ActionResult AddHotel(HotelAndPriceViewModel hotelAdd)
        {
            hotelAdd = SaveCategName(hotelAdd);
            try
            {
                if (ModelState.IsValid)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AddHotelHelpViewModel, AddHotelHelpDTO>()).CreateMapper();
                    var hotelDetails = mapper.Map<List<AddHotelHelpViewModel>, List<AddHotelHelpDTO>>(hotelAdd.HotelDetails);

                    var mapperVM = new MapperConfiguration(cfg => cfg.CreateMap<HotelAndPriceViewModel, HotelAndPriceDTO>()).CreateMapper();
                    var hotelToAdd = mapperVM.Map<HotelAndPriceViewModel, HotelAndPriceDTO>(hotelAdd);

                    hotelToAdd.HotelDetails = hotelDetails;

                    hotelService.AddHotel(hotelToAdd);

                    ViewBag.SuccessMsg = "Hotel added!";
                    return View(SaveCategName(hotelAdd));
                }
            }
            catch(ValidationException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(SaveCategName(hotelAdd));
        }

        private HotelAndPriceViewModel SaveCategName(HotelAndPriceViewModel hotel)
        {
            var categories = roomPriceService.GetRoomCategories();
            List<AddHotelHelpViewModel> helperList = new List<AddHotelHelpViewModel>();

            int i = 0;
            foreach (var item in categories)
            {
                helperList.Add(new AddHotelHelpViewModel { RoomCategoryID = item.RoomCategoryID, RoomCategoryName = item.Name, NumOfRooms = hotel.HotelDetails[i].NumOfRooms, Price = hotel.HotelDetails[i].Price });
                i++;
            }

            hotel.HotelDetails = helperList;
            return hotel;
        }

        public JsonResult DeleteHotel(int hotelID)
        {
            bool result = false;
            try
            {
                hotelService.DeleteHotel(hotelID);
                result = true;
                return Json(result);
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return Json(result);
            }
        }

        [HttpGet]
        public ActionResult ViewHotel(int? id)
        {
            try
            {
                var hotel = hotelService.ViewHotel(id);

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ViewHotelRoomPricesDTO, ViewHotelRoomPricesVM>()).CreateMapper();
                var hotelDetails = mapper.Map<List<ViewHotelRoomPricesDTO>, List<ViewHotelRoomPricesVM>>(hotel.Details);

                var mapperVM = new MapperConfiguration(cfg => cfg.CreateMap<ViewHotelDTO, ViewHotelViewModel>()).CreateMapper();
                var hotelVM = mapperVM.Map<ViewHotelDTO, ViewHotelViewModel>(hotel);

                hotelVM.Details = hotelDetails;

                return View(hotelVM);
            }
            catch(ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult AddBooking(int? id)
        {
            try
            {
                var hotel = hotelService.GetHotel(id);
                var booking = new FullBookingViewModel { HotelID = hotel.HotelID, HotelName = hotel.Name, FreeRooms = new List<Tuple<int, int, decimal>>() };

                return View(booking);
            }
            catch(ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AddBooking(FullBookingViewModel booking, string command)
        {
            booking.FreeRooms = new List<Tuple<int, int, decimal>>();
            try
            {
                if (command.Equals("Find free rooms"))
                {
                    var freeRooms = roomPriceService.GetRoomsByHotelAndDate(booking.HotelID, booking.StartDate, booking.EndDate);
                    booking.FreeRooms = freeRooms;
                }
                else
                {
                    booking.FreeRooms = roomPriceService.GetRoomsByHotelAndDate(booking.HotelID, booking.StartDate, booking.EndDate);

                    if (ModelState.IsValid)
                    {
                        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<FullBookingViewModel, FullBookingDTO>()).CreateMapper();
                        var fullBookingDto = mapper.Map<FullBookingViewModel, FullBookingDTO>(booking);

                        bookingService.BookRoom(fullBookingDto);
                        booking.FreeRooms = roomPriceService.GetRoomsByHotelAndDate(booking.HotelID, booking.StartDate, booking.EndDate);
                        ViewBag.SuccessMsg = "You successfully booked a room.";
                        return View(booking);
                    }
                
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                ViewBag.DatesError = ex.Message;
                return View(booking);
            }
            catch (RoomAlreadyBookedException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                ViewBag.RoomError = ex.Message;
                return View(booking);
            }
            return View(booking);
        }

        [HttpGet]
        public ActionResult DeleteBooking(int? id)
        {
            try
            {
                var hotel = hotelService.GetHotel(id);
                var cancelBooking = new CancelBookingViewModel { HotelID = hotel.HotelID, HotelName = hotel.Name };

                return View(cancelBooking);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteBooking(CancelBookingViewModel booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClientDTO client = new ClientDTO { Name = booking.Name, Surname = booking.Surname, BirthDate = booking.BirthDate };
                    ClientDTO newClient = clientService.GetClientByInfo(client);

                    var bookingDTO = new BookingDTO
                    {
                        HotelID = booking.HotelID,
                        RoomNumber = booking.RoomNumber,
                        ClientID = newClient.ClientID,
                        StartDate = booking.StartDate,
                        EndDate = booking.EndDate,
                        PeopleQuantity = booking.PeopleQuantity
                    };

                    bookingService.CancelBooking(bookingDTO);
                    ViewBag.SuccessMsg = "Successfully canceled booking.";
                    return View(booking);
                }
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                ViewBag.CancelError = ex.Message;
                return View(booking);
            }
            return View(booking);
        }

        [HttpGet]
        public ActionResult ViewBookedRooms(int? id)
        {
            try
            {
                var bookings = bookingService.GetBookedRooms(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ViewBookedRoomsDTO, ViewBookedRoomsVM>()).CreateMapper();
                var viewBookings = mapper.Map<ViewBookedRoomsDTO, ViewBookedRoomsVM>(bookings);
                return View(viewBookings);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult ViewFreeRooms(int? id)
        {
            try
            {
                var freeRooms = bookingService.GetFreeRooms(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ViewFreeRoomsDTO, ViewFreeRoomsVM>()).CreateMapper();
                var viewRooms = mapper.Map<ViewFreeRoomsDTO, ViewFreeRoomsVM>(freeRooms);
                return View(viewRooms);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult ViewRelatedClients(int? id)
        {
            try
            {
                var clients = clientService.GetClientsRelatedWithHotel(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ViewRelatedClientsDTO, ViewRelatedClientsVM>()).CreateMapper();
                var relatedClients = mapper.Map<ViewRelatedClientsDTO, ViewRelatedClientsVM>(clients);
                return View(relatedClients);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

    }
}