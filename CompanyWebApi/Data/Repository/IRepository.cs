using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.ASP.Data.Repository
{
    public interface IRepository<T>
    {
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetQuerable();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T input);
        void Remove(T toRemove);
        Task<bool> RemoveByIdAsync(int id);

        Task<bool> UpdateAsync(int id, T updated);
    }
}