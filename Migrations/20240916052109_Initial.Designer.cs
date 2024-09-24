﻿// <auto-generated />
using System;
using Fletes.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fletes.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240916052109_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Fletes.Models.Carrier", b =>
                {
                    b.Property<int>("CarrierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CarrierId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CarrierId");

                    b.ToTable("Carriers");
                });

            modelBuilder.Entity("Fletes.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Fletes.Models.Freight", b =>
                {
                    b.Property<int>("FreightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FreightId"));

                    b.Property<DateTime?>("ArrivalDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CarrierId")
                        .HasColumnType("integer");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RouteId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VehicleId")
                        .HasColumnType("integer");

                    b.HasKey("FreightId");

                    b.HasIndex("CarrierId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RouteId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Freights");
                });

            modelBuilder.Entity("Fletes.Models.FreightProduct", b =>
                {
                    b.Property<int>("FreightId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("FreightId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("FreightProducts");
                });

            modelBuilder.Entity("Fletes.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("InvoiceId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<int>("FreightId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FreightId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Fletes.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("VolumeM3")
                        .HasColumnType("numeric");

                    b.Property<decimal>("WeightKg")
                        .HasColumnType("numeric");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Fletes.Models.Route", b =>
                {
                    b.Property<int>("RouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RouteId"));

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("DistanceKm")
                        .HasColumnType("numeric");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RouteId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Fletes.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VehicleId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("VehicleId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Fletes.Models.Freight", b =>
                {
                    b.HasOne("Fletes.Models.Carrier", "Carrier")
                        .WithMany("Freights")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fletes.Models.Customer", "Customer")
                        .WithMany("Freights")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fletes.Models.Route", "Route")
                        .WithMany("Freights")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fletes.Models.Vehicle", "Vehicle")
                        .WithMany("Freights")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrier");

                    b.Navigation("Customer");

                    b.Navigation("Route");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Fletes.Models.FreightProduct", b =>
                {
                    b.HasOne("Fletes.Models.Freight", "Freight")
                        .WithMany("FreightProducts")
                        .HasForeignKey("FreightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fletes.Models.Product", "Product")
                        .WithMany("FreightProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Freight");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Fletes.Models.Invoice", b =>
                {
                    b.HasOne("Fletes.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fletes.Models.Freight", "Freight")
                        .WithMany("Invoices")
                        .HasForeignKey("FreightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Freight");
                });

            modelBuilder.Entity("Fletes.Models.Carrier", b =>
                {
                    b.Navigation("Freights");
                });

            modelBuilder.Entity("Fletes.Models.Customer", b =>
                {
                    b.Navigation("Freights");
                });

            modelBuilder.Entity("Fletes.Models.Freight", b =>
                {
                    b.Navigation("FreightProducts");

                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("Fletes.Models.Product", b =>
                {
                    b.Navigation("FreightProducts");
                });

            modelBuilder.Entity("Fletes.Models.Route", b =>
                {
                    b.Navigation("Freights");
                });

            modelBuilder.Entity("Fletes.Models.Vehicle", b =>
                {
                    b.Navigation("Freights");
                });
#pragma warning restore 612, 618
        }
    }
}
