﻿// <auto-generated />
using System;
using HMSMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HMSMVC.Migrations
{
    [DbContext(typeof(HMSDataContext))]
    [Migration("20240509201519_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HMSMVC.Entities.PatientServices", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("ServiceId");

                    b.ToTable("PatientServices");
                });

            modelBuilder.Entity("HMSMVC.Entities.Recommendation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("DoctorRecommendation1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DoctorRecommendation2")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DoctorRecommendation3")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DoctorRecommendation4")
                        .HasColumnType("longtext");

                    b.Property<string>("DoctorRecommendation5")
                        .HasColumnType("longtext");

                    b.Property<Guid>("MedicalRecordId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("MedicalRecordId")
                        .IsUnique();

                    b.ToTable("DoctorRecommendations");
                });

            modelBuilder.Entity("HMSMVC.Entity.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("AppointmentDateAndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Complain")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("DoctorId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("HMSMVC.Entity.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ClosingHours")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DepartmentDescription")
                        .HasColumnType("longtext");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HeadOfDepartment")
                        .HasColumnType("longtext");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("OpeningHours")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HMSMVC.Entity.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("DoctorCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FieldOfSpecialization")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SignatureUrl")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("YearOfExperience")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("HMSMVC.Entity.MedicalRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateRecorded")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DoctorReport")
                        .HasColumnType("longtext");

                    b.Property<string>("MedicalRecDept")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalRecords");
                });

            modelBuilder.Entity("HMSMVC.Entity.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("NotificationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RecipientEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SenderEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("HMSMVC.Entity.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("HasScheduleAppointment")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PatientCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("HMSMVC.Entity.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RefNumber")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("HMSMVC.Entity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("32161932-8a4b-4a14-bd08-32a69d295d1d"),
                            CreatedBy = "Admin",
                            CreatedOn = new DateTime(2024, 5, 9, 21, 15, 15, 550, DateTimeKind.Local).AddTicks(211),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("07f25da2-e3a9-4940-9759-3a5b86cbdbb0"),
                            CreatedBy = "System",
                            CreatedOn = new DateTime(2024, 5, 9, 21, 15, 15, 550, DateTimeKind.Local).AddTicks(270),
                            Name = "System"
                        },
                        new
                        {
                            Id = new Guid("37345650-7d02-4edc-a555-e2617575c738"),
                            CreatedBy = "Admin",
                            CreatedOn = new DateTime(2024, 5, 9, 21, 15, 15, 550, DateTimeKind.Local).AddTicks(275),
                            Name = "Manager"
                        },
                        new
                        {
                            Id = new Guid("7709b073-ddf7-4560-af43-a47b733415db"),
                            CreatedBy = "Admin",
                            CreatedOn = new DateTime(2024, 5, 9, 21, 15, 15, 550, DateTimeKind.Local).AddTicks(279),
                            Name = "Doctor"
                        },
                        new
                        {
                            Id = new Guid("f45f3ccb-cd78-434a-8e8b-749243af9122"),
                            CreatedBy = "Admin",
                            CreatedOn = new DateTime(2024, 5, 9, 21, 15, 15, 550, DateTimeKind.Local).AddTicks(282),
                            Name = "Patient"
                        });
                });

            modelBuilder.Entity("HMSMVC.Entity.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ServiceDescription")
                        .HasColumnType("longtext");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("HMSMVC.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("b56529d9-d708-4ef1-becb-c192d1559b75"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e7a95bab-64b8-4807-aa14-bbde9f9724f8",
                            CreatedBy = "Admin",
                            CreatedOn = new DateTime(2024, 5, 9, 21, 15, 15, 550, DateTimeKind.Local).AddTicks(799),
                            DateOfBirth = new DateTime(2000, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Admin",
                            Gender = 1,
                            LastName = "Admin",
                            LockoutEnabled = false,
                            Password = "Admin@1234",
                            PasswordHash = "AQAAAAIAAYagAAAAEArX//0LPXXWrIDiXI0l9RBu6qh/caSfV6Z4uhaPCkOFx5wOl8vLC8j8Ih1Ne4B9IQ==",
                            PhoneNumber = "08122423536",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "390088f1-139a-4396-87cc-ca4d8a78e596",
                            TwoFactorEnabled = false,
                            UserName = "admin@gmail.com"
                        },
                        new
                        {
                            Id = new Guid("6571c521-91ec-497d-a9b4-6b9b44179d29"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6c1f55b5-5683-4902-bfe9-d870fb6dc432",
                            CreatedBy = "System",
                            CreatedOn = new DateTime(2024, 5, 9, 21, 15, 15, 734, DateTimeKind.Local).AddTicks(4517),
                            DateOfBirth = new DateTime(2000, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "system@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "System",
                            Gender = 1,
                            LastName = "System",
                            LockoutEnabled = false,
                            Password = "System@1234",
                            PasswordHash = "AQAAAAIAAYagAAAAEIlWV61XiPPsvH9sU8jI/iWqnQCYaAJLWzww5nEywlBDCVKpecj6OIiCe4soUEpTfQ==",
                            PhoneNumber = "07038322233",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d4153fd7-fa26-4b21-9cde-33bc62c97567",
                            TwoFactorEnabled = false,
                            UserName = "system@gmail.com"
                        });
                });

            modelBuilder.Entity("HMSMVC.Entity.UserRoles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6cbb960f-62ee-415c-93c6-55335318eb3e"),
                            RoleId = new Guid("32161932-8a4b-4a14-bd08-32a69d295d1d"),
                            UserId = new Guid("b56529d9-d708-4ef1-becb-c192d1559b75")
                        },
                        new
                        {
                            Id = new Guid("63f241f3-77be-45d1-8a1b-cf7823830c98"),
                            RoleId = new Guid("07f25da2-e3a9-4940-9759-3a5b86cbdbb0"),
                            UserId = new Guid("6571c521-91ec-497d-a9b4-6b9b44179d29")
                        });
                });

            modelBuilder.Entity("HMSMVC.Entity.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId")
                        .IsUnique();

                    b.ToTable("Wallets");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7947ebae-f804-47ef-abc9-029caea58a3d"),
                            Balance = 0.0m
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HMSMVC.Entities.PatientServices", b =>
                {
                    b.HasOne("HMSMVC.Entity.Patient", "Patient")
                        .WithMany("PatientServices")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMSMVC.Entity.Service", "Service")
                        .WithMany("PatientServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("HMSMVC.Entities.Recommendation", b =>
                {
                    b.HasOne("HMSMVC.Entity.MedicalRecord", "MedicalRecord")
                        .WithOne("DoctorRecommendations")
                        .HasForeignKey("HMSMVC.Entities.Recommendation", "MedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalRecord");
                });

            modelBuilder.Entity("HMSMVC.Entity.Appointment", b =>
                {
                    b.HasOne("HMSMVC.Entity.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("HMSMVC.Entity.Doctor", "Doctor")
                        .WithMany("Appointment")
                        .HasForeignKey("DoctorId");

                    b.HasOne("HMSMVC.Entity.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMSMVC.Entity.Service", "Service")
                        .WithMany("Appointments")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("HMSMVC.Entity.Doctor", b =>
                {
                    b.HasOne("HMSMVC.Entity.Department", "Department")
                        .WithMany("Doctors")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMSMVC.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HMSMVC.Entity.MedicalRecord", b =>
                {
                    b.HasOne("HMSMVC.Entity.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMSMVC.Entity.Patient", null)
                        .WithMany("MedicalHistory")
                        .HasForeignKey("PatientId");

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("HMSMVC.Entity.Notification", b =>
                {
                    b.HasOne("HMSMVC.Entity.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HMSMVC.Entity.Patient", b =>
                {
                    b.HasOne("HMSMVC.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HMSMVC.Entity.Payment", b =>
                {
                    b.HasOne("HMSMVC.Entity.Patient", "Patient")
                        .WithMany("Payments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMSMVC.Entity.Service", "Service")
                        .WithMany("Payments")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("HMSMVC.Entity.UserRoles", b =>
                {
                    b.HasOne("HMSMVC.Entity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMSMVC.Entity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HMSMVC.Entity.Wallet", b =>
                {
                    b.HasOne("HMSMVC.Entity.Patient", "Patient")
                        .WithOne("Wallet")
                        .HasForeignKey("HMSMVC.Entity.Wallet", "PatientId");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("HMSMVC.Entity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("HMSMVC.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("HMSMVC.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("HMSMVC.Entity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMSMVC.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("HMSMVC.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HMSMVC.Entity.Department", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("HMSMVC.Entity.Doctor", b =>
                {
                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("HMSMVC.Entity.MedicalRecord", b =>
                {
                    b.Navigation("DoctorRecommendations")
                        .IsRequired();
                });

            modelBuilder.Entity("HMSMVC.Entity.Patient", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("MedicalHistory");

                    b.Navigation("PatientServices");

                    b.Navigation("Payments");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("HMSMVC.Entity.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("HMSMVC.Entity.Service", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("PatientServices");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("HMSMVC.Entity.User", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
