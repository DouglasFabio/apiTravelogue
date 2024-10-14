using ApiTravelogue.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Confluent.Kafka;
using System.Threading.Tasks;
using System.Collections.Generic;
using Confluent.Kafka.Admin;

namespace ApiTravelogue.Services
{
    public class EntradaServices
    {
        private readonly IMongoCollection<Entrada> _entradaCollection;
        private readonly IProducer<Null, string> _producer;
        private readonly AdminClientConfig _adminConfig;

        public EntradaServices(IOptions<ViagemDatabaseSettings> entradaServices, IProducer<Null, string> producer)
        {
            var mongoClient = new MongoClient(entradaServices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(entradaServices.Value.DatabaseName);
            _entradaCollection = mongoDatabase.GetCollection<Entrada>(entradaServices.Value.EntradaCollectionName);
            _producer = producer;
            _adminConfig = new AdminClientConfig { BootstrapServers = "localhost:9092" };

            CriarTopicoKafka().GetAwaiter().GetResult();
        }

        private async Task CriarTopicoKafka()
        {
            using var adminClient = new AdminClientBuilder(_adminConfig).Build();
            try
            {
                await adminClient.CreateTopicsAsync(new TopicSpecification[]
                {
                new TopicSpecification { Name = "notificacoes", NumPartitions = 1, ReplicationFactor = 1 }
                });
            }
            catch (CreateTopicsException e)
            {
                Console.WriteLine($"Erro ao criar o tópico: {e.Results[0].Error.Reason}");
            }
        }

        public async Task<List<Entrada>> GetImagens(string idEntry) =>
            await _entradaCollection.Find(i => i.Id == idEntry).ToListAsync();

        public async Task<List<Entrada>> GetEntradas() =>
            await _entradaCollection.Find(v => true).Sort(Builders<Entrada>.Sort.Descending(v => v.DateVisit)).ToListAsync();

        public async Task<List<Entrada>> GetEntrada(string idViagem) =>
            await _entradaCollection.Find(v => v.Id == idViagem).ToListAsync();

        public async Task<List<Entrada>> GetAsync(string idViagem) =>
            await _entradaCollection.Find(v => v.CodTravel == idViagem).ToListAsync();

        public async Task CreateAsync(Entrada entrada)
        {
            await _entradaCollection.InsertOneAsync(entrada);
            var mensagem = $"Nova entrada criada: {entrada.Id}";
            await _producer.ProduceAsync("notificacoes", new Message<Null, string> { Value = mensagem });
        }

        public async Task UpdateAsync(string id, Entrada entrada) =>
            await _entradaCollection.ReplaceOneAsync(v => v.Id == id, entrada);

        public async Task DeleteAsync(string id) =>
            await _entradaCollection.DeleteOneAsync(v => v.Id == id);
    }
}
