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
                (entradaServices.Value.EntradaCollectionName);
        }

        public async Task<List<Entrada>> GetImagens(string idEntry) => await _entradaCollection.Find(i => i.Id == idEntry).ToListAsync();
        
        public async Task<List<Entrada>> GetEntradas() => await _entradaCollection.Find(v => true).Sort(Builders<Entrada>.Sort.Descending(v => v.DateVisit)).ToListAsync();

        public async Task<List<Entrada>> GetAsync(string idViagem)
        {
            return await _entradaCollection.Find(v => v.CodTravel == idViagem).ToListAsync();
        }
        public async Task CreateAsync (Entrada entrada) => await _entradaCollection.InsertOneAsync(entrada);
        public async Task UpdateAsync (string id, Entrada entrada) => await _entradaCollection.ReplaceOneAsync(v=>v.Id == id, entrada);
        public async Task DeleteAsync (string id) => await _entradaCollection.DeleteOneAsync(v=>v.Id == id);
    }
}
