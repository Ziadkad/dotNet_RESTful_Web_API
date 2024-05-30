

using dotNet_RESTful_Web_API.Logging;

var builder = WebApplication.CreateBuilder(args);

// configuring Serilog to log in a file 
// Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/Userlogs.txt",rollingInterval: RollingInterval.Infinite).CreateLogger();

// doesn't work
// builder.Host.UseSerilog();

// Add services to the container.
// option.ReturnHttpNotAcceptable=true to not accept other types
// AddNewtonsoftJson so we can use patch 
//AddXmlDataContractSerializerFormatters() to change data to xml
builder.Services
     // .AddControllers(option => { option.ReturnHttpNotAcceptable = true; })
     .AddControllers()
     .AddNewtonsoftJson()
     .AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// custom logging
// explicit call so every object created with ILogging takes Logging
// Singleton same object for every time the application request an implementation // longest life time
// Scoped new  object for every request
// Transient new object every time object is accessed even within the same request
builder.Services.AddSingleton<ILogging, Logging>();
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