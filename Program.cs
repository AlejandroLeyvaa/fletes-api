using Fletes.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Fletes.Mapper;
using Fletes.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


var psqlConnection = builder.Configuration.GetConnectionString("psqlConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(psqlConnection));


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<FreightServices>();
builder.Services.AddScoped<FreightMappers>();

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
