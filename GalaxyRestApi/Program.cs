using GalaxyRestApi.DAL.Interfaces;
using GalaxyRestApi.DAL.Models;
using GalaxyRestApi.DAL.Models.Context;
using GalaxyRestApi.DAL.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddDbContext<EnterpriseContext>(opt =>
    opt.UseInMemoryDatabase("EnterpriseContext"));

builder.Services.AddTransient<IRepository<Employee>, Repository<Employee>>();
builder.Services.AddTransient<IRepository<Car>, Repository<Car>>();
builder.Services.AddTransient<IRepository<RegisteredEmployeeAuto>, Repository<RegisteredEmployeeAuto>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
