using LinqToDB;
using TicketService.DataAccessLayer.Entities;
using TicketService.DataAccessLayer.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace TicketService.DataAccessLayer.Repositories
{
    public class TicketRepository : BaseRepository<TicketEntity, int>, ITicketRepository
    {
        public TicketRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override async Task<TicketEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.TicketId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = CreateConnection(_configuration))
            {
                await db.GetTable<TicketEntity>().DeleteAsync(x => x.TicketId == id);
            }
        }

        public async Task<bool> CheckTicketExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.TicketId == identity))?.TicketId > 0;
        }

        public async Task DeleteBulkByCarIdAsync(IList<int> carIds)
        {
            using (var db = CreateConnection(_configuration))
            {
                await db.GetTable<TicketEntity>().DeleteAsync(x => carIds.Contains(x.CarId));
            }
        }
    }
}
