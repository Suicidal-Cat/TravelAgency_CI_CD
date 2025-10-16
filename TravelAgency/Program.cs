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

builder.Services.AddDbContext<AppDbContext>();
//repositories
builder.Services.AddScoped<IDestinationRepository, DestinationRepository>();

//services
builder.Services.AddScoped<IDestinationService, DestinationService>();

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
