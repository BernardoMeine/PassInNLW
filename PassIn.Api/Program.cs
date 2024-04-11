using Microsoft.EntityFrameworkCore;
using PassIn.Api.Filters;
using PassIn.Application.UseCases.Attendees.GetAll;
using PassIn.Application.UseCases.Attendees.Register;
using PassIn.Application.UseCases.CheckIns.Register;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Contracts;
using PassIn.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IAttendeeRepository, AttendeeRepository>();
builder.Services.AddScoped<GetAllAttendeesUseCase>();
builder.Services.AddScoped<RegisterAttendeeUseCase>();
builder.Services.AddScoped<ICheckInRepository, CheckInRepository>();
builder.Services.AddScoped<DoAttendeeCheckInUseCase>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<RegisterEventUseCase>();
builder.Services.AddScoped<GetEventByIdUseCase>();

builder.Services.AddDbContext<PassInDbContext>(options =>
{
    options.UseSqlite("Data Source=C:\\Users\\bemei\\Downloads\\PassInDb.db");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

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
