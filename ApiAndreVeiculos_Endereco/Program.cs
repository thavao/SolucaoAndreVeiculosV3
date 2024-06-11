using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ApiAndreVeiculos_Endereco.Data;
using Microsoft.CodeAnalysis.Options;
using ApiAndreVeiculos_Endereco;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApiAndreVeiculos_EnderecoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiAndreVeiculos_EnderecoContext") ?? throw new InvalidOperationException("Connection string 'ApiAndreVeiculos_EnderecoContext' not found.")));

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
//##
builder.Services.Configure<ApiAndreVeiculos_EnderecoMongoSettings>
    (builder.Configuration.GetSection(nameof(ApiAndreVeiculos_EnderecoMongoSettings)));

builder.Services.AddSingleton<IApiAndreVeiculos_EnderecoMongoSettings>
    (pera => pera.GetRequiredService<IOptions<ApiAndreVeiculos_EnderecoMongoSettings>>().Value);

builder.Services.AddSingleton<EnderecoService>();

//##

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
