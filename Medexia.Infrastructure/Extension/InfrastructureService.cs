using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;
using Medexia.Domain.Interfaces;
using Medexia.Infrastructure.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Medexia.Infrastructure.Extension
{
    public static class InfrastructureService
    {
        public static void AddInfrastructureservice(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<MedexiaContext>(options =>
            { options.UseSqlServer(config.GetConnectionString("cs")); });

            service.AddScoped<IAppointment, AppointmentRepo>();
            service.AddScoped<IDoctor, DoctorRepo>();
            service.AddScoped<IPatient, PatientRepo>();
            service.AddScoped<IRate, RateRepo>();
            service.AddScoped<ISpecialty, SpecialityRepo>();
            service.AddScoped<ITimeTable, TimeTableRepo>();
            service.AddScoped<IUserRepo, UserRepo>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IQueueItem , QueueRepo>();
            service.AddScoped<IQueueNotifier, QueueNotifierRepo>();
            service.AddScoped<IClinic , ClinicRepo>();

        }
        public async static Task AddRoles(this IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Admin", "Patient", "Doctor" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }


        }



    }
    }

