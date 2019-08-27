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
    public class ClientManagement : IClientManagement
    {
        IUnitOfWork Database { get; set; }

        public ClientManagement(IUnitOfWork unit)
        {
            Database = unit;
        }

        public bool AddClient(ClientDTO clientDto)
        {
            bool existingClient = false;
            Client cl = Database.Clients.Find(x => x.Name == clientDto.Name && x.Surname == clientDto.Surname && x.BirthDate == clientDto.BirthDate).FirstOrDefault();
            if (cl != null)
                existingClient = true;

            if (!existingClient)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, Client>()).CreateMapper();
                var client = mapper.Map<ClientDTO, Client>(clientDto);

                Database.Clients.Create(client);
                Database.Save();
            }

            return existingClient;
        }

        public ClientDTO GetClientByInfo(ClientDTO clientDto)
        {
            Client client = Database.Clients.Find(x => x.Name == clientDto.Name && x.Surname == clientDto.Surname && x.BirthDate == clientDto.BirthDate).FirstOrDefault();
            if (client != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
                return mapper.Map<Client, ClientDTO>(client);
            }
            else
                throw new ValidationException("This client does not exist.", "");
        }

        public void DeleteClient(ClientDTO clientDto)
        {
            var bookings = Database.Bookings.Find(x => x.ClientID == clientDto.ClientID);
            if(bookings != null)
                Database.Bookings.DeleteRange(bookings);

            Database.Clients.Delete(clientDto.ClientID);
            Database.Save();
        }

        public void EditClient(ClientDTO clientDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, Client>()).CreateMapper();
            var client = mapper.Map<ClientDTO, Client>(clientDto);

            Database.Clients.Update(client);
            Database.Save();
        }

        public IEnumerable<ClientDTO> GetClients()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Client>, List<ClientDTO>>(Database.Clients.GetAll());
        }

        public ClientDTO ViewClient(int? id)
        {
            if (id == null)
                throw new ValidationException("ID of client is not set", "");
            var client = Database.Clients.Get(id.Value);
            if (client == null)
                throw new ValidationException("Client was not found", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();

            return mapper.Map<Client, ClientDTO>(client);
        }

        public IEnumerable<ClientDTO> SortByName(IEnumerable<ClientDTO> clients)
        {
            if (clients == null)
                throw new ValidationException("The client list is empty", "");

            var sortedClients = from client in clients
                                orderby client.Name
                                select client;

            return sortedClients;
        }

        public IEnumerable<ClientDTO> SortBySurname(IEnumerable<ClientDTO> clients)
        {
            if (clients == null)
                throw new ValidationException("The client list is empty", "");

            var sortedClients = from client in clients
                                orderby client.Surname
                                select client;

            return sortedClients;
        }

        public IEnumerable<ClientDTO> GetByClientInfo(string info)
        {
            if (info.Trim() == "")
                throw new ValidationException("Set the input textbox.", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Client>, List<ClientDTO>>(Database.Clients.Find(x => x.Name.ToLower().Contains(info.ToLower()) || x.Surname.ToLower().Contains(info.ToLower())));
        }

        public ViewRelatedClientsDTO GetClientsRelatedWithHotel(int? hotelID)
        {
            if (hotelID == null)
                throw new ValidationException("ID of hotel is not set", "");

            Hotel hotel = Database.Hotels.Get(hotelID.Value);
            if (hotel == null)
                throw new ValidationException("Hotel was not found.", "");

            var clients = Database.Bookings.Find(x => x.HotelID == hotelID).Select(x => x.Client).Distinct();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, HotelDTO>()).CreateMapper();

            var relatedClients =  mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(clients);

            var returnObj = new ViewRelatedClientsDTO
            {
                Hotel = mapper2.Map<Hotel, HotelDTO>(hotel),
                Clients = relatedClients
            };

            return returnObj;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
