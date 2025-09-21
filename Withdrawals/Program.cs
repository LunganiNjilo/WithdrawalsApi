using Withdrawals.Application.Interfaces;
using Withdrawals.Application.Services;
using Withdrawals.Infrustructure.Messaging;
using Withdrawals.Infrustructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Register dependencies
builder.Services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();
builder.Services.AddSingleton<IEventPublisher, InMemoryEventPublisher>();
builder.Services.AddScoped<IWithdrawalService, WithdrawalService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
