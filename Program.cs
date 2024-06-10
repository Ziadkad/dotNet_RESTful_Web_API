

using System.Text;
using Asp.Versioning;
using dotNet_RESTful_Web_API.Data;
using dotNet_RESTful_Web_API.Logging;
using dotNet_RESTful_Web_API.Mapper;
using dotNet_RESTful_Web_API.Repository;
using dotNet_RESTful_Web_API.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IUserRepository, UserRepository>();  
builder.Services.AddScoped<IUserNumberRepository, UserNumberRepository>();
builder.Services.AddScoped<ILocalUserRepository, LocalUserRepository>();
//versionning
builder.Services.AddApiVersioning(option =>
{
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new ApiVersion(1, 0);
});
// configuring Serilog to log in a file 
// Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/Userlogs.txt",rollingInterval: RollingInterval.Infinite).CreateLogger();
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
    });
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
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
            "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();