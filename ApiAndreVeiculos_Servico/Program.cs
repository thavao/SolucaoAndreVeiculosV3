using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApiAndreVeiculos_Servico.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApiAndreVeiculos_ServicoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiAndreVeiculos_ServicoContext") ?? throw new InvalidOperationException("Connection string 'ApiAndreVeiculos_ServicoContext' not found.")));

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
