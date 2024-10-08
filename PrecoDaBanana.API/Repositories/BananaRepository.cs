using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PrecoDaBanana.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrecoDaBanana.API.Repositories
{
    public class BananaRepository : IBananaRepository
    {
        private readonly IMongoCollection<Banana> _bananas;

        public BananaRepository(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _bananas = database.GetCollection<Banana>("Bananas");
        }

        public async Task<List<Banana>> GetAllAsync()
        {
            return await _bananas.Find(b => true).ToListAsync();
        }

        public async Task<Banana> GetByIdAsync(string id)
        {
            return await _bananas.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Banana banana)
        {
            await _bananas.InsertOneAsync(banana);
        }

        public async Task UpdateAsync(string id, Banana banana)
        {
            await _bananas.ReplaceOneAsync(b => b.Id == id, banana);
        }

        public async Task DeleteAsync(string id)
        {
            await _bananas.DeleteOneAsync(b => b.Id == id);
        }
    }
}
