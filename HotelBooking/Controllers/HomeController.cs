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
    public class HomeController : Controller
    {
        IBookingService bookingService;

        public HomeController(IBookingService service)
        {
            bookingService = service;
        }

        //public ActionResult Index()
        //{
        //    IEnumerable<HotelDTO> hotelDtos = bookingService.GetHotels();
        //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HotelDTO, HotelViewModel>()).CreateMapper();
        //    var hotels = mapper.Map<IEnumerable<HotelDTO>, List<HotelViewModel>>(hotelDtos);
        //    return View(hotels);
        //}

        //public ActionResult BookARoom(int? id)
        //{
        //    try
        //    {
        //        HotelDTO hotel = bookingService.GetHotel(id);
        //        var booking = new BookingViewModel { HotelID = hotel.HotelID };

        //        return View(booking);
        //    }
        //    catch(ValidationException ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //}

        //[HttpPost]
        //public ActionResult BookARoom(BookingViewModel booking)
        //{
        //    try
        //    {
        //        var bookingDto = new BookingDTO { HotelID = booking.HotelID, RoomNumber = booking.RoomNumber, ClientID = 1, StartDate = booking.StartDate, EndDate = booking.EndDate, PeopleQuantity = booking.PeopleQuantity, BookingDate = booking.BookingDate, TotalPrice = booking.TotalPrice };
        //        bookingService.BookRoom(bookingDto);
        //        return Content("<h2>Book completed!</h2>");
        //    }
        //    catch(ValidationException ex)
        //    {
        //        ModelState.AddModelError(ex.Property, ex.Message);
        //    }
        //    return View(booking);
        //}

        protected override void Dispose(bool disposing)
        {
            bookingService.Dispose();
            base.Dispose(disposing);
        }
    }
}