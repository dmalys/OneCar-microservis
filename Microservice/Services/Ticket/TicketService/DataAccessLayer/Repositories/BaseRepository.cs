using LinqToDB;
using TicketService.DataAccessLayer.Entities;
using TicketService.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TicketService.DataAccessLayer
{
    public abstract class BaseRepository<T, I> : IBaseRepository<T, I>
            where T : class, IBaseEntity
    {
        protected readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected OneCarDbConnection CreateConnection(IConfiguration configuration)
        {
            return new OneCarDbConnection(configuration.GetConnectionString("TicketsConn"));
        }

        public async Task<I> Insert(T entity)
        {
            using (var db = CreateConnection(_configuration))
            {
                var identity = await db.InsertWithIdentityAsync(entity);
                return (I)Convert.ChangeType(identity, typeof(I));
            }
        }

        public async Task<int> Update(T entity)
        {
            using (var db = CreateConnection(_configuration))
            {
                return await db.UpdateAsync(entity);
            }
        }

        public async Task<int> CountByCriteria(Expression<Func<T, bool>> where)
        {
            using (var db = CreateConnection(_configuration))
            {
                return await db.GetTable<T>().CountAsync(where);
            }
        }

        public async Task<int> Delete(T entity)
        {
            using (var db = CreateConnection(_configuration))
            {
                return await db.DeleteAsync(entity);
            }
        }

        public abstract Task<T> GetAsync(I identity);

        public virtual async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            using (var db = CreateConnection(_configuration))
            {
                var table = db.GetTable<T>();
                return await table.Where(where).SingleOrDefaultAsync();
            }
        }

        public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            using (var db = CreateConnection(_configuration))
            {
                var table = db.GetTable<T>();
                return await table.Where(where).FirstOrDefaultAsync();
            }
        }

        public async Task<List<T>> GetAll()
        {
            using (var db = CreateConnection(_configuration))
            {
                return await db.GetTable<T>().ToListAsync();
            }
        }
    }
}
