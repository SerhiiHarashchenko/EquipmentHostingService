﻿// <auto-generated />
using EquipmentHostingService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EquipmentHostingService.Data.Migrations
{
    [DbContext(typeof(EquipmentHostingServiceDbContext))]
    partial class EquipmentHostingServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EquipmentHostingService.Data.Entities.EquipmentPlacementContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EquipmentQuantity")
                        .HasColumnType("int");

                    b.Property<string>("EquipmentTypeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FacilityCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentTypeCode");

                    b.HasIndex("FacilityCode")
                        .HasDatabaseName("IX_EquipmentPlacementContract_FacilityCode");

                    b.ToTable("EquipmentPlacementContracts");
                });

            modelBuilder.Entity("EquipmentHostingService.Data.Entities.EquipmentType", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Area")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Code");

                    b.ToTable("EquipmentTypes");
                });

            modelBuilder.Entity("EquipmentHostingService.Data.Entities.Facility", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("StandardArea")
                        .HasColumnType("int");

                    b.HasKey("Code");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("EquipmentHostingService.Data.Entities.EquipmentPlacementContract", b =>
                {
                    b.HasOne("EquipmentHostingService.Data.Entities.EquipmentType", "EquipmentType")
                        .WithMany()
                        .HasForeignKey("EquipmentTypeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EquipmentHostingService.Data.Entities.Facility", "Facility")
                        .WithMany()
                        .HasForeignKey("FacilityCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EquipmentType");

                    b.Navigation("Facility");
                });
#pragma warning restore 612, 618
        }
    }
}