using ApiTravelogue.Models;
using ApiTravelogue.Services;
using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ViagemDatabaseSettings>
    (builder.Configuration.GetSection("TravelogueStoreData"));

builder.Services.Configure<ViagemDatabaseSettings>(builder.Configuration.GetSection("TravelogueStoreData"));
builder.Services.AddSingleton<ViagemServices>();
builder.Services.AddSingleton<EntradaServices>();
builder.Services.AddControllers();


var kafkaConfig = new ProducerConfig { BootstrapServers = "localhost:9092", SecurityProtocol = SecurityProtocol.Plaintext };
builder.Services.AddSingleton<IProducer<Null, string>>(new ProducerBuilder<Null, string>(kafkaConfig).Build());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP
   // options.ListenAnyIP(7298, listenOptions => // HTTPS
   // {
   //     listenOptions.UseHttps();
   // });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
