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
    public class ClientController : Controller
    {
        IHotelManagement hotelService;
        IRoomPriceManagement roomPriceService;
        IClientManagement clientService;
        IBookingService bookingService;

        public ClientController(IHotelManagement service, IRoomPriceManagement service2, IClientManagement service3, IBookingService service4)
        {
            hotelService = service;
            roomPriceService = service2;
            clientService = service3;
            bookingService = service4;
        }

        public ActionResult Index()
        {
            IEnumerable<ClientDTO> clientDtos = clientService.GetClients();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>()).CreateMapper();
            var clients = mapper.Map<IEnumerable<ClientDTO>, IEnumerable<ClientViewModel>>(clientDtos);
            return View(clients);
        }

        [HttpGet]
        public ActionResult AddClient()
        {
            var clientView = new ClientViewModel { BirthDate = new DateTime(1998, 9, 10) };
            return View(clientView);
        }

        [HttpPost]
        public ActionResult AddClient(ClientViewModel clientAdd)
        {
            if (ModelState.IsValid)
            {
                ClientDTO client = new ClientDTO { Name = clientAdd.Name, Surname = clientAdd.Surname, BirthDate = clientAdd.BirthDate, Phone = clientAdd.Phone, Email = clientAdd.Email };
                if (clientService.AddClient(client))
                {
                    ViewBag.ErrorMsg = "The exact same client is already in the database.";
                    return View(clientAdd);
                }
                else
                {
                    ViewBag.SuccessMsg = "Client successfully added.";
                    return View(clientAdd);
                }
            }
            return View(clientAdd);
        }

        [HttpGet]
        public ActionResult DeleteClient(int? id)
        {
            try
            {
                var client = clientService.ViewClient(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>()).CreateMapper();

                return View(mapper.Map<ClientDTO, ClientViewModel>(client));
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteClient(ClientViewModel client)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>()).CreateMapper();

            clientService.DeleteClient(mapper.Map<ClientViewModel, ClientDTO>(client));
            ViewBag.SuccessMsg = "Client successfully deleted.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditClient(int? id)
        {
            try
            {
                var client = clientService.ViewClient(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>()).CreateMapper();

                return View(mapper.Map<ClientDTO, ClientViewModel>(client));
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult EditClient(ClientViewModel client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>()).CreateMapper();

                    clientService.EditClient(mapper.Map<ClientViewModel, ClientDTO>(client));
                    return View(client);
                }
            }
            catch (ValidationException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(client);
        }

        [HttpPost]
        public ActionResult Index(IList<ClientViewModel> clients, string command, string clientInfo)
        {
            if(command.Equals("Sort by client name"))
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>()).CreateMapper();
                var mapperReverse = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>()).CreateMapper();

                var sortedClients = clientService.SortByName(mapper.Map<IList<ClientViewModel>, List<ClientDTO>>(clients));
                var afterMap = mapperReverse.Map<IEnumerable<ClientDTO>, IList<ClientViewModel>>(sortedClients);
                return View("Index", afterMap);
            }
            else if(command.Equals("Sort by client surname"))
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>()).CreateMapper();
                var mapperReverse = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>()).CreateMapper();

                var sortedClients = clientService.SortBySurname(mapper.Map<IList<ClientViewModel>, List<ClientDTO>>(clients));
                var afterMap = mapperReverse.Map<IEnumerable<ClientDTO>, IList<ClientViewModel>>(sortedClients);
                return View("Index", afterMap);
            }
            else
            {
                Session["input"] = clientInfo;
                IEnumerable<ClientDTO> clientDtos;
                try
                {
                    clientDtos = clientService.GetByClientInfo(clientInfo);
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>()).CreateMapper();
                    var resultClients = mapper.Map<IEnumerable<ClientDTO>, IList<ClientViewModel>>(clientDtos);
                    return View("Index", resultClients);
                }
                catch (ValidationException ex)
                {
                    ViewBag.Error = ex.Message;
                    if (clients == null)
                    {
                        return View(new List<ClientViewModel>());
                    }
                    return View("Index", clients);
                }
            }
        }
    }
}