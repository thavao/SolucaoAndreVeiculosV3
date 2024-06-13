using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using _01_ApiAndreVeiculos_Geral.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<_01_ApiAndreVeiculos_GeralContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("_01_ApiAndreVeiculos_GeralContext") ?? throw new InvalidOperationException("Connection string '_01_ApiAndreVeiculos_GeralContext' not found.")));

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
