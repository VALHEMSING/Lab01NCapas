using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProxyServer.Interfaces
{
    public interface ISuppliersProxy
    {
        Task<Supplier> CreateAsync(Supplier supplier);
        Task<bool> DeleteAsync(int id);
        Task<List<Supplier>> GetAllAsync();
        Task<Supplier> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, Supplier supplier);
    }
}