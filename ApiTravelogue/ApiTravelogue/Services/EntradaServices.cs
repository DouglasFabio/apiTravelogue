using ApiTravelogue.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiTravelogue.Services
{
    public class EntradaServices
    {
        private readonly IMongoCollection<Entrada> _entradaCollection;

        public EntradaServices(IOptions<ViagemDatabaseSettings> entradaServices)
        {
            var mongoClient = new MongoClient(entradaServices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(entradaServices.Value.DatabaseName);

            _entradaCollection = mongoDatabase.GetCollection<Entrada>
                (entradaServices.Value.ViagemCollectionName);
        }

        public async Task<List<Entrada>> GetAsync() => await _entradaCollection.Find(v => true).Sort(Builders<Entrada>.Sort.Descending(v => v.DateVisit)).ToListAsync();
        public async Task<Entrada> GetAsync(string id) => await _entradaCollection.Find(v => v.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync (Entrada viagem) => await _entradaCollection.InsertOneAsync(viagem);
        public async Task UpdateAsync (string id, Entrada viagem) => await _entradaCollection.ReplaceOneAsync(v=>v.Id == id, viagem);
        public async Task DeleteAsync (string id) => await _entradaCollection.DeleteOneAsync(v=>v.Id == id);
    }
}
