using Microsoft.EntityFrameworkCore;
using SaatApi.Business.Services;
using SaatApiCore.Interfaces;
using SaatApiDataAccess.Context;
using SaatApiDataAccess.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ISaatRepository, SaatRepository>();
builder.Services.AddScoped<IKayitRepository, KayitRepository>();
builder.Services.AddScoped<ISaatService, SaatService>();
builder.Services.AddScoped<IKayitService, KayitService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();


app.Run();
