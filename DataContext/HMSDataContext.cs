using HMSMVC.Entities;
using HMSMVC.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

namespace HMSMVC.Data
{
    public class HMSDataContext : IdentityDbContext<User, Role, Guid>
    {
        public HMSDataContext(DbContextOptions<HMSDataContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Recommendation> DoctorRecommendations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = new Guid("32161932-8a4b-4a14-bd08-32a69d295d1d"), Name = "Admin", CreatedBy = "Admin", CreatedOn = DateTime.Now },
                new Role { Id = new Guid("07f25da2-e3a9-4940-9759-3a5b86cbdbb0"), Name = "System", CreatedBy = "System", CreatedOn = DateTime.Now },
                new Role { Id = new Guid("37345650-7d02-4edc-a555-e2617575c738"), Name = "Manager", CreatedBy = "Admin", CreatedOn = DateTime.Now },
                new Role { Id = new Guid("7709b073-ddf7-4560-af43-a47b733415db"), Name = "Doctor", CreatedBy = "Admin", CreatedOn = DateTime.Now },
                new Role { Id = new Guid("f45f3ccb-cd78-434a-8e8b-749243af9122"), Name = "Patient", CreatedBy = "Admin", CreatedOn = DateTime.Now }
            );

            // Hash passwords
            var passwordHasher = new PasswordHasher<User>();

            // Seed default users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = new Guid("b56529d9-d708-4ef1-becb-c192d1559b75"),
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    PhoneNumber = "08122423536",
                    Password = "Admin@1234",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(2000, 08, 23),
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now,
                    PasswordHash = passwordHasher.HashPassword(null, "Admin@1234"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = new Guid("6571c521-91ec-497d-a9b4-6b9b44179d29"),
                    FirstName = "System",
                    LastName = "System",
                    Email = "system@gmail.com",
                    UserName = "system@gmail.com",
                    PhoneNumber = "07038322233",
                    Password = "System@1234",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(2000, 08, 23),
                    CreatedBy = "System",
                    CreatedOn = DateTime.Now,
                    PasswordHash = passwordHasher.HashPassword(null, "System@1234"),
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            );

            // Associate roles with users
            modelBuilder.Entity<UserRoles>().HasData(
                new UserRoles { Id = Guid.NewGuid(), UserId = new Guid("b56529d9-d708-4ef1-becb-c192d1559b75"), RoleId = new Guid("32161932-8a4b-4a14-bd08-32a69d295d1d") }, // Admin
                new UserRoles { Id = Guid.NewGuid(), UserId = new Guid("6571c521-91ec-497d-a9b4-6b9b44179d29"), RoleId = new Guid("07f25da2-e3a9-4940-9759-3a5b86cbdbb0") }  // System
            );

            modelBuilder.Entity<Wallet>().HasData(
                new Wallet
                {
                    Id = new Guid("7947ebae-f804-47ef-abc9-029caea58a3d"),
                    Balance = 0.0m,
                }
            );
        }
    }

}
