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
