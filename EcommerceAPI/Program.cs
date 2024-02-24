using EcommerceAPI.Data;
using EcommerceAPI.Interfaces.Respository;
using EcommerceAPI.Interfaces.Service;
using EcommerceAPI.Repository;
using EcommerceAPI.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(x => x.AddPolicy("Mypolicy", builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();
}
));

builder.Services.AddDbContext<EcommerceContext>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();


var app = builder.Build();

var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (envName == "Production")
{
    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<EcommerceContext>();
    db.Database.Migrate();
}





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var sid = builder.Configuration.GetSection("TwilioSid").Value;
    var token = builder.Configuration.GetSection("TwilioToken").Value;

    Environment.SetEnvironmentVariable("TwilioToken", token);
    Environment.SetEnvironmentVariable("TwilioSid", sid);
}





app.UseCors("Mypolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
