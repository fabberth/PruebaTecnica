using CRUD.DbContext;
using CRUD.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Buscando la cadena de conexión
var appsettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var DefaultConnection = $"{Environment.GetEnvironmentVariable("DB_CONNECTION")}"; // DOCKER
if (string.IsNullOrEmpty(DefaultConnection))
{
    DefaultConnection = appsettings["ConnectionStrings:DefaultConnection"];
}
if (string.IsNullOrEmpty(DefaultConnection))
{
    throw new Exception("\"DefaultConnection\" key not found");
}

// Configurar DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(DefaultConnection));

builder.Services.AddScoped<IAutorRepository, AutorRepository>();

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
