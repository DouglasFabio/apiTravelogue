using ApiTravelogue.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiTravelogue.Services
{
    public class ViagemServices
    {
        private readonly IMongoCollection<Viagem> _viagemCollection;

        public ViagemServices(IOptions<ViagemDatabaseSettings> produtoServices)
        {
            var mongoClient = new MongoClient(produtoServices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(produtoServices.Value.DatabaseName);

            _viagemCollection = mongoDatabase.GetCollection<Viagem>
                (produtoServices.Value.ViagemCollectionName);
        }

        public async Task<List<Viagem>> GetAsync() => await _viagemCollection.Find(v => true).ToListAsync();
        public async Task<Viagem> GetAsync(string id) => await _viagemCollection.Find(v => v.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync (Viagem viagem) => await _viagemCollection.InsertOneAsync(viagem);
        public async Task UpdateAsync (string id, Viagem viagem) => await _viagemCollection.ReplaceOneAsync(v=>v.Id == id, viagem);
        public async Task DeleteAsync (string id) => await _viagemCollection.DeleteOneAsync(v=>v.Id == id);
    }
}
