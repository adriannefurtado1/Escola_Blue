using Escola.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Contexto de desenvolvimento
//builder.Services.AddDbContext<EscolaContext>(opt => opt.UseInMemoryDatabase("Escola"));
//builder.Services.AddDbContext<EscolaContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(@"Data Source = (LocalDb)\MSSQLLocalDB; Initial Catalog = escola; Persist Security Info=True; User ID = root; Password = 123456789"));

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<EscolaContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
