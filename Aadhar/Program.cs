
using Aadhar.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin();
    });
});

// Add services to the container.
builder.Services.AddDbContext<UserDataDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Apply CORS middleware
app.UseCors("AllowAngularOrigins");
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();

