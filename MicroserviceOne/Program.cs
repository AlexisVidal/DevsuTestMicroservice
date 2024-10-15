using MicroserviceOne.Data;
using MicroserviceOne.Repositories;
using MicroserviceOne.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // No cambiar el nombre de las propiedades
    });
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

//Configuracion Rabbitmq
builder.Services.AddSingleton<IConnection>(sp =>
{
    var connFactory = new ConnectionFactory() { HostName = "localhost" };
    return connFactory.CreateConnection();
});
// Registra IModel
builder.Services.AddSingleton<IModel>(sp =>
{
    var connection = sp.GetRequiredService<IConnection>();
    return connection.CreateModel();
});
// Registra RabbitMQPublisher
builder.Services.AddSingleton<RabbitMQPublisher>(sp =>
{
    var connection = sp.GetRequiredService<IConnection>(); // Ya registrado previamente
    var channel = connection.CreateModel();
    return new RabbitMQPublisher(channel);
});


// Registra RabbitMQResponder (aseg�rate de que use IModel)
builder.Services.AddSingleton<RabbitMQResponder>(sp =>
{
    var channel = sp.GetRequiredService<IModel>();
    var repository = sp.GetRequiredService<ClienteRepository>(); // Aseg�rate de que esto est� registrado
    var publisher = sp.GetRequiredService<RabbitMQPublisher>();
    return new RabbitMQResponder(channel, repository, publisher);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.MapControllers();
//app.UseHttpsRedirection();
app.Run();

