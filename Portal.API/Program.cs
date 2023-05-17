
using Common.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Portal.API.Extenstions;
using Portal.Application.Interfaces;
using Portal.Domain.Common;
using Portal.Domain.Entities;
using Portal.Infrastructure;
using Portal.Infrastructure.Datas;
using Portal.Infrastructure.Implements;
using Serilog;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
var builder = WebApplication.CreateBuilder(args);
Log.Information($"Start {builder.Environment.ApplicationName} up");

try
{
  // Add services to the container.
  builder.Host.AddAppConfigurations();
  builder.Services.AddControllers();
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddIdentity<Portal.Domain.Entities.AppUser, Portal.Domain.Entities.AppRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();
  Assembly[] assemblies = new[] { Assembly.GetAssembly(typeof(IServiceScoped)),
                  Assembly.GetAssembly(typeof(Program)) };
  builder.Services.Scan(scan => scan
        .FromAssemblies(assemblies)
        .AddClasses(classes => classes.AssignableTo<IServiceScoped>())
             .AsImplementedInterfaces()
             .WithSingletonLifetime());
  builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
  builder.Services.AddSingleton<ICurrentContext,CurrentContext>();
  builder.Services.AddSwaggerGen(c =>
  {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
      Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
      Name = "Authorization",
      In = ParameterLocation.Header,
      Type = SecuritySchemeType.ApiKey,
      Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                  });
  });
  string issuer = builder.Configuration.GetValue<string>("Tokens:Issuer") ?? "";
  string signingKey = builder.Configuration.GetValue<string>("Tokens:Key") ?? "";
  byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);
  builder.Services.AddAuthentication(opt =>
  {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  })
            .AddJwtBearer(options =>
            {
              options.RequireHttpsMetadata = false;
              options.SaveToken = true;
              options.TokenValidationParameters = new TokenValidationParameters()
              {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = issuer,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = System.TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
              };
            });
  builder.Services.ConfigureServices();
  builder.Services.ConfigAutoMapper();
  builder.Services.ConfigCORS();
  builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();
  builder.Services.AddAuthorization(options =>
  {
    options.AddPolicy("Permission", policy =>
                policy.Requirements.Add(new PermissionRequirement()));
  });
  //builder.Services.AddSwaggerGen();


  var app = builder.Build();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger();
    app.UseSwaggerUI();
  }

  app.UseHttpsRedirection();
  app.UseCors("CorsPolicy");

  app.UseAuthentication();
  app.UseAuthorization();

  app.MapControllers();

  app.Run();

}
catch (Exception ex)
{
  string type = ex.GetType().Name;
  if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

  Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
  Log.Information($"Shut down {builder.Environment.ApplicationName} complete");
  Log.CloseAndFlush();
}


