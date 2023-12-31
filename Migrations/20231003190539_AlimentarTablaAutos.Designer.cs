﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoPractica_API.Datos;

#nullable disable

namespace ProyectoPracticaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231003190539_AlimentarTablaAutos")]
    partial class AlimentarTablaAutos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProyectoPractica_API.Modelos.Proyecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("Hp")
                        .HasColumnType("int");

                    b.Property<string>("ImagenUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Precio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Autos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FechaActualizacion = new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5957),
                            FechaCreacion = new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5975),
                            Hp = 135,
                            ImagenUrl = "",
                            Marca = "Volkswagen",
                            Nombre = "Tiguan",
                            Precio = 539000
                        },
                        new
                        {
                            Id = 2,
                            FechaActualizacion = new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5981),
                            FechaCreacion = new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5982),
                            Hp = 500,
                            ImagenUrl = "",
                            Marca = "Cupra",
                            Nombre = "Ateca",
                            Precio = 959000
                        },
                        new
                        {
                            Id = 3,
                            FechaActualizacion = new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5987),
                            FechaCreacion = new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5989),
                            Hp = 525,
                            ImagenUrl = "",
                            Marca = "Porsche",
                            Nombre = "911 GT3-RS",
                            Precio = 4778000
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
