﻿// <auto-generated />
using System;
using ManageUsers.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ManageUsers.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ManageUsers.Domain.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Administrators");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ApplicationUserId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950a"),
                            FirstName = "Admin",
                            LastName = "Admin",
                            Patronymic = "Admin"
                        });
                });

            modelBuilder.Entity("ManageUsers.Domain.ApplicationUser", b =>
                {
                    b.Property<Guid>("ApplicationUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ApplicationUserRole")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApplicationUserId");

                    b.ToTable("ApplicationUsers");

                    b.HasData(
                        new
                        {
                            ApplicationUserId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950a"),
                            ApplicationUserRole = 1,
                            Login = "Admin",
                            PasswordHash = "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv"
                        },
                        new
                        {
                            ApplicationUserId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950d"),
                            ApplicationUserRole = 3,
                            Login = "Doctor1",
                            PasswordHash = "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv"
                        },
                        new
                        {
                            ApplicationUserId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950b"),
                            ApplicationUserRole = 2,
                            Login = "Patient1",
                            PasswordHash = "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv"
                        });
                });

            modelBuilder.Entity("ManageUsers.Domain.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("CabinetNumber")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateBirthday")
                        .HasColumnType("datetime2");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Address = "Doctor1Address",
                            ApplicationUserId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950d"),
                            CabinetNumber = 1,
                            Category = "High",
                            DateBirthday = new DateTime(2024, 4, 20, 18, 21, 6, 472, DateTimeKind.Local).AddTicks(5705),
                            Experience = 1,
                            FirstName = "Doctor1FirstName",
                            LastName = "Doctor1LastName",
                            Patronymic = "Doctor1Patronymic",
                            Phone = "Doctor1Phone"
                        });
                });

            modelBuilder.Entity("ManageUsers.Domain.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateBirthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Address = "Patient1Address",
                            ApplicationUserId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950b"),
                            Avatar = "",
                            DateBirthday = new DateTime(2024, 4, 20, 18, 21, 6, 472, DateTimeKind.Local).AddTicks(6614),
                            FirstName = "Patient1FirstName",
                            LastName = "Patient1LastName",
                            Patronymic = "Patient1Patronymic",
                            Phone = "Patient1Phone"
                        });
                });

            modelBuilder.Entity("ManageUsers.Domain.Administrator", b =>
                {
                    b.HasOne("ManageUsers.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("ManageUsers.Domain.Doctor", b =>
                {
                    b.HasOne("ManageUsers.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("ManageUsers.Domain.Patient", b =>
                {
                    b.HasOne("ManageUsers.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });
#pragma warning restore 612, 618
        }
    }
}
