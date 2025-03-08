using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using TaskManagement.Api;
using TaskManagement.Api.Domain.Commands.v1;
using TaskManagement.Api.Domain.Contracts.Repositories;
using TaskManagement.Api.Domain.Contracts.Services;
using TaskManagement.Api.Domain.Services;
using TaskManagement.Api.Infra.Data.Connections;
using TaskManagement.Api.Infra.Data.Repositories;
using TaskManagement.Api.Infra.IO.Services;
using TaskManagement.Api.Middlewares;
using TaskManagement.Api.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<SqliteHostedService>();

builder.Services.AddHealthChecks();
builder.Services
   .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
      var jwtIssuer = builder.Configuration["Jwt:Issuer"];
      var jwtKey = builder.Configuration["Jwt:Key"];

      options.RequireHttpsMetadata = false;
      options.SaveToken = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)),
         ClockSkew = TimeSpan.Zero
      };
   });
builder.Services
   .AddCors(o => o.AddDefaultPolicy(builder => builder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader()
      .Build())
   )
   .AddAutoMapper(typeof(Program))
   .AddSwaggerGen(c =>
   {
      c.SwaggerDoc(builder.Configuration["SwaggerGen:Name"], new OpenApiInfo
      {
         Title = builder.Configuration["SwaggerGen:OpenApiInfo:Title"],
         Version = builder.Configuration["SwaggerGen:OpenApiInfo:Version"],
         Description = builder.Configuration["SwaggerGen:OpenApiInfo:Description"]
      });
      c.AddSecurityDefinition(builder.Configuration["SwaggerGen:OpenApiSecurityScheme:Scheme"], new OpenApiSecurityScheme
      {
         Name = builder.Configuration["SwaggerGen:OpenApiSecurityScheme:Name"],
         Type = SecuritySchemeType.ApiKey,
         Scheme = builder.Configuration["SwaggerGen:OpenApiSecurityScheme:Scheme"],
         BearerFormat = builder.Configuration["SwaggerGen:OpenApiSecurityScheme:BearerFormat"],
         In = ParameterLocation.Header,
         Description = builder.Configuration["SwaggerGen:OpenApiSecurityScheme:Description"]
      });
      c.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
         {
            new OpenApiSecurityScheme
            {
               Reference = new OpenApiReference
               {
                  Type = ReferenceType.SecurityScheme,
                  Id = builder.Configuration["SwaggerGen:OpenApiSecurityScheme:Scheme"]
               }
            },
            Array.Empty<string>()
         }
      });
   })
   .AddMediatR(msc => msc.RegisterServicesFromAssemblyContaining<AbstractCommand>())
   .AddScoped<IAuthenticationService, AuthenticationService>()
   .AddScoped<IFileWriter, FileWriter>()
   .AddScoped<IHighPriorityTaskEventHandler, HighPriorityTaskEventHandler>()
   .AddScoped(provider =>
      new MapperConfiguration(cfg =>
         cfg.AddProfile(new TaskProfile(provider.GetRequiredService<IHighPriorityTaskEventHandler>()))
      )
      .CreateMapper()
   )
   .AddSingleton<IConnectionProvider, SqliteConnectionProvider>()
   .AddSingleton<ITaskRepository, TaskRepository>()
   .AddProblemDetails()
   .AddControllers()
   .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();

var app = builder.Build();

app
   .UseExceptionHandler()
   .UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
   app.MapOpenApi();
}

app
   .UseSwagger()
   .UseSwaggerUI(c => c.SwaggerEndpoint(builder.Configuration["SwaggerEndpoint:Url"], builder.Configuration["SwaggerEndpoint:Name"]))
   .UseCors();

app.UseHttpsRedirection();

app.UseMiddleware<RequestLoggingMiddleware>();

app
   .UseAuthentication()
   .UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

await app.RunAsync();

public partial class Program { }
