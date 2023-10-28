using LibraryAPI_Mauricio.DAL;
using LibraryAPI_Mauricio.Domain.Interfaces;
using LibraryAPI_Mauricio.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// LINEA PARA CONFIGURAR CONEXION DB
builder.Services.AddDbContext<DataBaseContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// POR CADA NUEVO SERVICIO/INTERFAZ EN LA API SE DEBE AGREGAR AQUI LA NUEVA DEPENDENCIA 
builder.Services.AddScoped<IBookService, BookService>();


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
