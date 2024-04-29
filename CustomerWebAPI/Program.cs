using CustomerWebAPI.Data;
using CustomerWebAPI.RabbitMQ;
using CustomerWebAPI.Repositories.Interfaces;
using CustomerWebAPI.Repositories.SQLCode;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = "Server=localhost;Database=customer;Port=5432;User Id=postgres;Password=root";

builder.Services.AddDbContext<CustomerDbContext>(option =>
{
    option.EnableSensitiveDataLogging(true);
    option.UseNpgsql(connectionString);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRabbitMQConnection>(new RabbitMQConnection());
builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();

//Services
builder.Services.AddScoped<ICustomer, CustomerRepo>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

app.Run();

void ApplyMigration()
{
    using var scope = app.Services.CreateScope();

    var _db = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();

    if (_db.Database.GetPendingMigrations().Any())
    {
        _db.Database.Migrate();
    }
}