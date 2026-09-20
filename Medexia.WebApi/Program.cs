
using System.Text;
using System.Threading.RateLimiting;
using FluentValidation;
using Medexia.Application.Extension;
using Medexia.Application.Rates.Query.GetRates_of_any_doctor;
using Medexia.Application.Timetable.Command.SetTimeTable;
using Medexia.Application.user.Register.AsDoctor;
using Medexia.Application.ValdiationBehavior;
using Medexia.Domain.Entities;
using Medexia.Infrastructure;
using Medexia.Infrastructure.Extension;
using Medexia.WebApi.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Medexia.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services
            builder.Services.AddControllers();
            builder.Services.AddSignalR();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            //  app.MapControllers():
            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 8 Web API",
                    Description = " ITI Projrcy"
                });
                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text in..."
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Password.RequireDigit = true;

                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<MedexiaContext>();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .ToDictionary(
                                kvp => kvp.Key,
                                kvp => kvp.Value.Errors.Select(e =>
                                   // ???? ??? ???? mapping ??????? ?????????? ?????? ???? ?????
                                   ValdiatorHelper.CustomizeErrorMessage(kvp.Key, e.ErrorMessage)
                                ).ToArray()
                            );

                        var response = new
                        {
                            Message = "Validation failed",
                            Errors = errors
                        };

                        return new BadRequestObjectResult(response);
                    };
                });

            builder.Services.AddRateLimiter(options =>
            {
                options.AddPolicy("RateLimiter", HttpContent => RateLimitPartition
                .GetFixedWindowLimiter(
                    partitionKey: HttpContent.User?.Identity?.Name
                    ?? HttpContent.Connection.RemoteIpAddress?.ToString(), factory: _ => new FixedWindowRateLimiterOptions
                    {
                        Window = TimeSpan.FromSeconds(10),
                        PermitLimit = 5,
                        QueueLimit = 0

                        
                    }));
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            });
            builder.Services.AddAuthentication(options => //to change the deafult of compiling with cookies to compiling with tokens 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.RequireHttpsMetadata = false; options.SaveToken = true;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Iss"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Aud"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))


                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/hubs/queue"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddInfrastructureservice(builder.Configuration);
            builder.Services.AddApplicationService();
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterAsDocValidation>();
            builder.Services.AddValidatorsFromAssemblyContaining<SetTimeTableValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<GetRateValidator>();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                await scope.ServiceProvider.AddRoles();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");        
            app.UseAuthentication();
            app.UseRateLimiter();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapHub<QueueHub>("/hubs/queue");  
            app.MapControllers();


            app.MapControllers();

            app.Run();
        }
    }
}
