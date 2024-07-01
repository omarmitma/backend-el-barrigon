using Microsoft.EntityFrameworkCore;
using Proyecto__Final.Tablas;
using System.Text.Json.Serialization;
using Proyecto__Final.hubs;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddDbContext<SistemaRestaurante>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("conexionsql")));

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


builder.Services.AddCors(opt =>
{

    opt.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200", "http://localhost:60679")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
    });

    //opt.AddDefaultPolicy(builder =>
    //{
    //    builder
    //        .WithOrigins("http://localhost:4200") // Reemplaza con el origen correcto de tu aplicación Angular
    //        .AllowAnyMethod()
    //        .AllowAnyHeader()
    //        .AllowCredentials()
    //        .SetIsOriginAllowed(_ => true);
    //});
});


var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();
app.MapHub<llamarMesero>("/hub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
