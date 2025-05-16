using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Configurations;
using MyVaccine.WebApi.Configurations.Injections;
using MyVaccine.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configuration = builder.Configuration;

builder.Services.SetDatabaseConfiguration(configuration);
builder.Services.SetMyVaccineAuthConfiguration();
builder.Services.SetDependencyInjection();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//ESTE CODIGO ES  UTILIZADO PARA OBLIGAR AL SERVICIO A FUNCIONAR LOCALMENTE EN EL PUERTO 38791 PERO SI TIENES EL CONTENEDOR
// Y EL SERVICIO VA A HABER CONFLICTO  POR QUE USAN EL MISMO PUERTO ENTONCES EN ESE CASO NO ES NECESARIO 

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(38791); // Cambia a ListenAnyIP para asegurarte de que pueda aceptar peticiones externas
//});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyVaccineAppDbContext>();
    try
    {
        // Aplica las migraciones pendientes
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
        // Considera si deber�as detener el arranque de la aplicaci�n aqu� o no.
    }
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

// app.UseHttpsRedirection();
app.UseDeveloperExceptionPage();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
