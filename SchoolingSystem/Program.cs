using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using SchoolingSystem.Models;
using SchoolingSystem.Repositories;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registered Dtos and Domain classes mapping service
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Database connection
builder.Services.AddDbContextPool<SchoolDbContext>(options 
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Registered Authentication and Authorization services
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SchoolDbContext>();

//Configured Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 0;

});

//Registered Student Repositories into dependency injection services
builder.Services.AddScoped<IStudentRepositories, StudentRepositories>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseStatusCodePages(context =>
    {
        var response = context.HttpContext.Response;

        if (response.StatusCode == (int)HttpStatusCode.NotFound)
            response.WriteAsync("Invalid address or api endpoint Not found");

        return Task.CompletedTask;
    });
}
else
{
    app.UseStatusCodePages(context =>
    {
        var response = context.HttpContext.Response;
        
        if (response.StatusCode == (int)HttpStatusCode.NotFound)
            response.WriteAsync("Invalid address or api endpoint Not found");        

        return Task.CompletedTask;
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.UseAuthorization();


app.Run();
