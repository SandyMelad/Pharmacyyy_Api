using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy_ASP_API.Models;
using Microsoft.VisualBasic;
using Pharmacy_ASP_API.Repositories;
using System.Diagnostics.Metrics;
using Pharmacy_ASP_API.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IRepositories<MedicationKnowledge>, MedicationKnowledgeRepo>();
builder.Services.AddScoped<IRepositories<MedicationRequest>, MedicationRequestRepo>();
builder.Services.AddScoped<IRepositories<Order>, OrderRepo>();
builder.Services.AddScoped<IRepositories<Patient>,PatientRepo>();
builder.Services.AddScoped<IRepositories<Report>, ReportpRepo>();
builder.Services.AddScoped<IRepositories<Stock>, StockRepo>();



builder.Services.AddDbContext<PharmacyDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0))));

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
