using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.BLL.DTO;

namespace HotelBooking.BLL.Interfaces
{
    public interface IClientManagement
    {
        bool AddClient(ClientDTO clientDto);
        void DeleteClient(ClientDTO clientDto);
        void EditClient(ClientDTO clientDto);
        ClientDTO ViewClient(int? id);
        IEnumerable<ClientDTO> GetClients();
        ClientDTO GetClientByInfo(ClientDTO clientDto);
        IEnumerable<ClientDTO> SortByName(IEnumerable<ClientDTO> clients);
        IEnumerable<ClientDTO> SortBySurname(IEnumerable<ClientDTO> clients);
        IEnumerable<ClientDTO> GetByClientInfo(string info);
        ViewRelatedClientsDTO GetClientsRelatedWithHotel(int? hotelID);
        void Dispose();
    }
}
