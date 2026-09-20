using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Medexia.Infrastructure
{
    public class MedexiaContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }
        public DbSet<_Rate> Rates { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<QueueItem> QueueItems { get; set; }
        public DbSet<Clinic> clinic { get; set; }


        public MedexiaContext(DbContextOptions<MedexiaContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Specialty>().HasData(
                new Specialty { Id = 1, Name = "GeneralPractice", Description = "طب عام" },
                new Specialty { Id = 2, Name = "InternalMedicine", Description = "باطنة" },
                new Specialty { Id = 3, Name = "Cardiology", Description = "قلب" },
                new Specialty { Id = 4, Name = "Dermatology", Description = "جلدية" },
                new Specialty { Id = 5, Name = "Pediatrics", Description = "أطفال" },
                new Specialty { Id = 6, Name = "ObstetricsAndGynecology", Description = "نساء وتوليد" },
                new Specialty { Id = 7, Name = "Orthopedics", Description = "عظام" },
                new Specialty { Id = 8, Name = "Neurology", Description = "مخ وأعصاب" },
                new Specialty { Id = 9, Name = "Psychiatry", Description = "طب نفسي" },
                new Specialty { Id = 10, Name = "Ophthalmology", Description = "رمد" },
                new Specialty { Id = 11, Name = "ENT", Description = "أنف وأذن وحنجرة" },
                new Specialty { Id = 12, Name = "Dentistry", Description = "أسنان" },
                new Specialty { Id = 13, Name = "Urology", Description = "مسالك بولية" },
                new Specialty { Id = 14, Name = "GeneralSurgery", Description = "جراحة عامة" },
                new Specialty { Id = 15, Name = "PlasticSurgery", Description = "جراحة تجميل" },
                new Specialty { Id = 16, Name = "Neurosurgery", Description = "جراحة مخ وأعصاب" },
                new Specialty { Id = 17, Name = "CardiothoracicSurgery", Description = "جراحة قلب وصدر" },
                new Specialty { Id = 18, Name = "VascularSurgery", Description = "جراحة أوعية دموية" },
                new Specialty { Id = 19, Name = "Oncology", Description = "أورام" },
                new Specialty { Id = 20, Name = "Endocrinology", Description = "غدد صماء" },
                new Specialty { Id = 21, Name = "Gastroenterology", Description = "جهاز هضمي" },
                new Specialty { Id = 22, Name = "Pulmonology", Description = "صدرية" },
                new Specialty { Id = 23, Name = "Nephrology", Description = "كلى" },
                new Specialty { Id = 24, Name = "Rheumatology", Description = "روماتيزم" },
                new Specialty { Id = 25, Name = "Hematology", Description = "أمراض الدم" },
                new Specialty { Id = 26, Name = "InfectiousDiseases", Description = "أمراض معدية" },
                new Specialty { Id = 27, Name = "Anesthesiology", Description = "تخدير" },
                new Specialty { Id = 28, Name = "Radiology", Description = "أشعة" },
                new Specialty { Id = 29, Name = "Pathology", Description = "باثولوجي" },
                new Specialty { Id = 30, Name = "EmergencyMedicine", Description = "طوارئ" }
            );
        }

        //عايزين نشوف الحاجات الفي program 
    }
}
