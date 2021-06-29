﻿// <auto-generated />
using System;
using Glomad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Glomad.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191011115801_AddVisaAcceptance")]
    partial class AddVisaAcceptance
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Glomad.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryIata")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Iata")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InternetSpeed")
                        .HasColumnType("int");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NomadScore")
                        .HasColumnType("int");

                    b.Property<string>("PhotoName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeZone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Glomad.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DevelopmentLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISOalpha2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISOalpha3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("ISOnumric")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Glomad.Models.Embassy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("CountryIata")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessingTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecialConditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkingHours")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("Embassy");
                });

            modelBuilder.Entity("Glomad.Models.Visa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryIata")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("Duration")
                        .HasColumnType("smallint");

                    b.Property<int?>("EmbassyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnArrival")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("EmbassyId");

                    b.ToTable("Visa");
                });

            modelBuilder.Entity("Glomad.Models.VisaAcceptance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryIata")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("VisaId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CountryId");

                    b.HasIndex("VisaId");

                    b.ToTable("VisaAcceptance");
                });

            modelBuilder.Entity("Glomad.Models.Embassy", b =>
                {
                    b.HasOne("Glomad.Models.City", "City")
                        .WithMany("Embassies")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Glomad.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Glomad.Models.Visa", b =>
                {
                    b.HasOne("Glomad.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("Glomad.Models.Embassy", "Embassy")
                        .WithMany()
                        .HasForeignKey("EmbassyId");
                });

            modelBuilder.Entity("Glomad.Models.VisaAcceptance", b =>
                {
                    b.HasOne("Glomad.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Glomad.Models.Visa", "Visa")
                        .WithMany()
                        .HasForeignKey("VisaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
