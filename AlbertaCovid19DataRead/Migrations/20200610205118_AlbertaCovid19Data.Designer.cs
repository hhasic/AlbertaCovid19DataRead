﻿// <auto-generated />
using System;
using AlbertaCovid19DataRead;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AlbertaCovid19DataRead.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200610205118_AlbertaCovid19Data")]
    partial class AlbertaCovid19Data
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AlbertaCovid19DataRead.Area", b =>
                {
                    b.Property<int>("AreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GeometryData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("AreaId");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("AlbertaCovid19DataRead.AreaCovidInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Active")
                        .HasColumnType("int");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<int>("Cases")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deaths")
                        .HasColumnType("int");

                    b.Property<int>("Recovered")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("AreaCovidInfo");
                });

            modelBuilder.Entity("AlbertaCovid19DataRead.LabTesting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CalgaryZone")
                        .HasColumnType("int");

                    b.Property<int>("CentralZone")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EdmontonZone")
                        .HasColumnType("int");

                    b.Property<int>("NorthZone")
                        .HasColumnType("int");

                    b.Property<int>("SouthZone")
                        .HasColumnType("int");

                    b.Property<int>("UnknownZone")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LabTesting");
                });

            modelBuilder.Entity("AlbertaCovid19DataRead.AreaCovidInfo", b =>
                {
                    b.HasOne("AlbertaCovid19DataRead.Area", "Area")
                        .WithMany("AreaCovidData")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
