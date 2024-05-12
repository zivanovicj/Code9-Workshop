using Code9.Domain.Commands;
using Code9.Domain.Interfaces;
using Code9.Infrastructure;
using Code9.Infrastructure.Repositories;
using Code9WebAPI.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CinemaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CinemaConnStr")));

builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AddCinemaCommand).Assembly));

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<AddCinemaValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    using var dbContext = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
