using EcommerceAPI.Data;
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

var app = builder.Build();

var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if(envName == "Production")
{
    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<EcommerceContext>();
    db.Database.Migrate();
    app.Run();
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
