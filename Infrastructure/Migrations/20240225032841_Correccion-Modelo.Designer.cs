﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ChallengeContext))]
    [Migration("20240225032841_Correccion-Modelo")]
    partial class CorreccionModelo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EmployeePermissionsEmployee", b =>
                {
                    b.Property<int>("EmployeesId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionEmployeesId")
                        .HasColumnType("int");

                    b.HasKey("EmployeesId", "PermissionEmployeesId");

                    b.HasIndex("PermissionEmployeesId");

                    b.ToTable("EmployeePermissionsEmployee");
                });

            modelBuilder.Entity("Infrastructure.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkAreaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkAreaId")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Infrastructure.Models.PermissionsEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("PermissionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermissionTypeId")
                        .IsUnique();

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Infrastructure.Models.PermissionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PermissionsTypes");
                });

            modelBuilder.Entity("Infrastructure.Models.WorkArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AreaName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkAreas");
                });

            modelBuilder.Entity("EmployeePermissionsEmployee", b =>
                {
                    b.HasOne("Infrastructure.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.PermissionsEmployee", null)
                        .WithMany()
                        .HasForeignKey("PermissionEmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Models.Employee", b =>
                {
                    b.HasOne("Infrastructure.Models.WorkArea", "WorkArea")
                        .WithOne("Employee")
                        .HasForeignKey("Infrastructure.Models.Employee", "WorkAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkArea");
                });

            modelBuilder.Entity("Infrastructure.Models.PermissionsEmployee", b =>
                {
                    b.HasOne("Infrastructure.Models.PermissionType", "PermissionTypes")
                        .WithOne("PermisssionsEmployees")
                        .HasForeignKey("Infrastructure.Models.PermissionsEmployee", "PermissionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PermissionTypes");
                });

            modelBuilder.Entity("Infrastructure.Models.PermissionType", b =>
                {
                    b.Navigation("PermisssionsEmployees");
                });

            modelBuilder.Entity("Infrastructure.Models.WorkArea", b =>
                {
                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
