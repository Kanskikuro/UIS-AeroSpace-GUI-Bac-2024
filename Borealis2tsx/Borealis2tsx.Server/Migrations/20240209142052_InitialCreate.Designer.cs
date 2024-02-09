﻿// <auto-generated />
using Borealis2tsx.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Borealis2tsx.Server.Migrations
{
    [DbContext(typeof(DataLineDbContext))]
    [Migration("20240209142052_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Borealis2tsx.Server.Interfaces.DataLine", b =>
                {
                    b.Property<string>("AccX")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccZ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Altitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GyroX")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GyroY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GyroZ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MagX")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MagY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MagZ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pressure")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Temperature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("DataLine");
                });
#pragma warning restore 612, 618
        }
    }
}
