using Microsoft.EntityFrameworkCore;
using ProyectoPractica_API;
using ProyectoPractica_API.Datos;
using ProyectoPractica_API.Repositorio;
using ProyectoPractica_API.Repositorio.IRepositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(); //Activacion requerida para los 2 paquetes NuGet instalados.  
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registro de conexi�n con BDD
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Agregado de nuevo servicio para mapeo:
builder.Services.AddAutoMapper(typeof(MappingConfig));

//Registro para la parte de servicio
builder.Services.AddScoped<IProyectoRepositorio, ProyectoRepositorio>();
builder.Services.AddScoped<INumeroProyectoRepositorio, NumeroProyectoRepositorio>();

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


//Comentario prueba