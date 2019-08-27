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
    public class BookingController : Controller
    {
        IHotelManagement hotelService;
        IRoomPriceManagement roomPriceService;
        IClientManagement clientService;
        IBookingService bookingService;

        public BookingController(IHotelManagement service, IRoomPriceManagement service2, IClientManagement service3, IBookingService service4)
        {
            hotelService = service;
            roomPriceService = service2;
            clientService = service3;
            bookingService = service4;
        }

        public List<BookingViewModel> GetAllBookings()
        {
            var bookingDtos = bookingService.GetBookings();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, BookingViewModel>()).CreateMapper();
            return mapper.Map<IEnumerable<BookingDTO>, List<BookingViewModel>>(bookingDtos);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var bookingDtos = bookingService.GetBookings();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, BookingViewModel>()).CreateMapper();
            var bookings = mapper.Map<IEnumerable<BookingDTO>, List<BookingViewModel>>(bookingDtos);
            return View(bookings);
        }

        [HttpPost]
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            Session["startDate"] = startDate;
            Session["endDate"] = endDate;
            IEnumerable<BookingDTO> bookingDtos;
            List<BookingViewModel> bookings = GetAllBookings();
            try
            {
                bookingDtos = bookingService.GetBookingsByDate(startDate, endDate);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, BookingViewModel>()).CreateMapper();
                bookings = mapper.Map<IEnumerable<BookingDTO>, List<BookingViewModel>>(bookingDtos);
            }
            catch (ValidationException ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(bookings);
        }

        [HttpGet]
        public ActionResult ViewBooking(int? id)
        {
            try
            {
                BookingDTO booking = bookingService.GetBooking(id);

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, BookingViewModel>()).CreateMapper();
                var bookingVM = new BookingViewModel();

                bookingVM = mapper.Map<BookingDTO, BookingViewModel>(booking);

                return View(bookingVM);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}