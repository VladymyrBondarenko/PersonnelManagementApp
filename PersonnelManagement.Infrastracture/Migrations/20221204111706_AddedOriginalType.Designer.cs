﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonnelManagement.Infrastracture.DbContexts;

#nullable disable

namespace PersonnelManagement.Infrastracture.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221204111706_AddedOriginalType")]
    partial class AddedOriginalType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc.1.22426.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Departments.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartmentDescription")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("DepartmentTitle")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Employees.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EmployeeState")
                        .HasColumnType("int");

                    b.Property<DateTime>("FireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PositionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Models.Identity.RefreshToken", b =>
                {
                    b.Property<Guid>("Token")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Invalidated")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("JwtId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Token");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Models.Originals.Original", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileName")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OriginalFileExtension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalTitle")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("OriginalType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OrderId");

                    b.ToTable("Originals");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("OrderDescriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrderState")
                        .HasColumnType("int");

                    b.Property<Guid?>("PositionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OrderDescriptionId");

                    b.HasIndex("PositionId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Orders.OrderDescription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("OrderDescriptionTitle")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("OrderType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OrdersDescription");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Positions.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("PositionDescription")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("PositionTitle")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Models.Identity.IdentityUserModel", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("EmployeeId")
                        .IsUnique()
                        .HasFilter("[EmployeeId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("IdentityUserModel");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Employees.Employee", b =>
                {
                    b.HasOne("PersonnelManagement.Domain.Departments.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PersonnelManagement.Domain.Positions.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Department");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Models.Identity.RefreshToken", b =>
                {
                    b.HasOne("PersonnelManagement.Domain.Models.Identity.IdentityUserModel", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Models.Originals.Original", b =>
                {
                    b.HasOne("PersonnelManagement.Domain.Employees.Employee", "Employee")
                        .WithMany("Originals")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PersonnelManagement.Domain.Orders.Order", "Order")
                        .WithMany("Originals")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Employee");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Orders.Order", b =>
                {
                    b.HasOne("PersonnelManagement.Domain.Departments.Department", "Department")
                        .WithMany("Orders")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PersonnelManagement.Domain.Employees.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PersonnelManagement.Domain.Orders.OrderDescription", "OrderDescription")
                        .WithMany("Orders")
                        .HasForeignKey("OrderDescriptionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Domain.Positions.Position", "Position")
                        .WithMany("Orders")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Department");

                    b.Navigation("Employee");

                    b.Navigation("OrderDescription");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Models.Identity.IdentityUserModel", b =>
                {
                    b.HasOne("PersonnelManagement.Domain.Employees.Employee", "Employee")
                        .WithOne("User")
                        .HasForeignKey("PersonnelManagement.Domain.Models.Identity.IdentityUserModel", "EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Departments.Department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Employees.Employee", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Originals");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Orders.Order", b =>
                {
                    b.Navigation("Originals");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Orders.OrderDescription", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Positions.Position", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Models.Identity.IdentityUserModel", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
