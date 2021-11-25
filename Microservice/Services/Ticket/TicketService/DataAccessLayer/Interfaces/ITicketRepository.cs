using TicketService.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketService.DataAccessLayer.Interfaces
{
    public interface ITicketRepository
    {
        Task<TicketEntity> GetAsync(int identity);
        Task<int> Insert(TicketEntity ticket);

        Task<List<TicketEntity>> GetAll();

        Task DeleteAsync(int id);

        Task<bool> CheckTicketExists(int identity);
        Task DeleteBulkByCarIdAsync(IList<int> carIds);
    }
}
