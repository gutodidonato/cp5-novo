using System.Collections.Generic;
using System.Threading.Tasks;
using PrecoDaBanana.API.Models;

namespace PrecoDaBanana.API.Repositories
{
    public interface IBananaRepository
    {
        Task<List<Banana>> GetAllAsync();
        Task<Banana> GetByIdAsync(string id);
        Task CreateAsync(Banana banana);
        Task UpdateAsync(string id, Banana banana);
        Task DeleteAsync(string id);
    }
}
