using DataBase;
using Repositories.Implementation;
using Repositories.Interfaces;
using Services;
using Services.Implementation;
using Services.Interfaces;
using TravelAgency.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// datebase
builder.Services.AddDbContext<AppDbContext>();

//repositories
builder.Services.AddScoped<IDestinationRepository, DestinationRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

//services
builder.Services.AddScoped<IDestinationService, DestinationService>();
builder.Services.AddScoped<ITripService, TripService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseMiddleware<ApiExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
