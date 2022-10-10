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
    [Migration("20221010100809_PersonnelManagementDb_Departments_AddedCreatedDate")]
    partial class PersonnelManagementDbDepartmentsAddedCreatedDate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc.1.22426.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PersonnelManagement.Domain.Departments.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 10, 10, 8, 9, 739, DateTimeKind.Utc).AddTicks(8538));

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

            modelBuilder.Entity("PersonnelManagement.Domain.Models.Originals.Original", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OriginalFileExtension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalTitle")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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

                    b.Property<string>("PositionTitle")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
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

            modelBuilder.Entity("PersonnelManagement.Domain.Departments.Department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Employees.Employee", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Originals");
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
#pragma warning restore 612, 618
        }
    }
}
