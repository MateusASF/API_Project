using API_Final_Project.Core.Interfaces;
using API_Final_Project.Core.Service;
using API_Final_Project.Filters;
using API_Final_Project.Infra.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//interfaces
builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
builder.Services.AddScoped<ICityEventService, CityEventService>();
builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();
builder.Services.AddScoped<IEventReservationService, EventReservationService>();

//filters
builder.Services.AddMvc(options => options.Filters.Add<GeneralExceptionFilter>()); // => Global Filter

builder.Services.AddScoped<LogActionFilter_RegistroExistente_City>();
builder.Services.AddScoped<LogActionFilter_RegistroExistente_Reservation>();
builder.Services.AddScoped<LogActionFilter_AdmUser_City>();

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
