using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouponService.DataAccessLayer
{
    public interface IBaseRepository<T, I> where T : class
    {
        Task<I> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
        Task<T> GetAsync(I identity);
        Task<List<T>> GetAll();
    }
}
