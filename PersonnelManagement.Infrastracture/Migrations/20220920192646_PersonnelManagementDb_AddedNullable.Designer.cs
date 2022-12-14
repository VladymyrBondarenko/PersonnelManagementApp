// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PersonnelManagement.Infrastracture.DbContexts;

#nullable disable

namespace PersonnelManagement.Infrastracture.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220920192646_PersonnelManagementDb_AddedNullable")]
    partial class PersonnelManagementDbAddedNullable
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

                    b.Property<string>("DepartmentTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Employees.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DepartmentId")
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

                    b.Property<Guid>("PositionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId")
                        .IsUnique();

                    b.HasIndex("PositionId")
                        .IsUnique();

                    b.ToTable("Employees");
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

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderDescriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrderState")
                        .HasColumnType("int");

                    b.Property<Guid>("PositionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId")
                        .IsUnique();

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OrderDescriptionId")
                        .IsUnique();

                    b.HasIndex("PositionId")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Orders.OrderDescription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderDescriptionTitle")
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Employees.Employee", b =>
                {
                    b.HasOne("PersonnelManagement.Domain.Departments.Department", "Department")
                        .WithOne()
                        .HasForeignKey("PersonnelManagement.Domain.Employees.Employee", "DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Domain.Positions.Position", "Position")
                        .WithOne()
                        .HasForeignKey("PersonnelManagement.Domain.Employees.Employee", "PositionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Orders.Order", b =>
                {
                    b.HasOne("PersonnelManagement.Domain.Departments.Department", "Department")
                        .WithOne()
                        .HasForeignKey("PersonnelManagement.Domain.Orders.Order", "DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Domain.Employees.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Domain.Orders.OrderDescription", "OrderDescription")
                        .WithOne()
                        .HasForeignKey("PersonnelManagement.Domain.Orders.Order", "OrderDescriptionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Domain.Positions.Position", "Position")
                        .WithOne()
                        .HasForeignKey("PersonnelManagement.Domain.Orders.Order", "PositionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Employee");

                    b.Navigation("OrderDescription");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("PersonnelManagement.Domain.Employees.Employee", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
