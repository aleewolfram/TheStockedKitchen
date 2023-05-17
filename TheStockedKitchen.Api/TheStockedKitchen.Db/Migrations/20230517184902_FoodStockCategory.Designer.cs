﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheStockedKitchen.Db;

#nullable disable

namespace TheStockedKitchen.DB.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20230517184902_FoodStockCategory")]
    partial class FoodStockCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TheStockedKitchen.Data.Model.FoodStock", b =>
                {
                    b.Property<int>("FoodStockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FoodStockId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodStockId"), 1L, 1);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Category");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Image");

                    b.Property<DateTime?>("LastEditedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastEditedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<double>("Quantity")
                        .HasColumnType("float")
                        .HasColumnName("Quantity");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Unit");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("User");

                    b.HasKey("FoodStockId");

                    b.ToTable("FoodStock", "dbo");
                });

            modelBuilder.Entity("TheStockedKitchen.Data.Model.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UnitId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnitId"), 1L, 1);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Abbreviation");

                    b.Property<bool>("AllowedInDropDown")
                        .HasColumnType("bit")
                        .HasColumnName("AllowedInDropDown");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("UnitId");

                    b.ToTable("Unit", "dbo");

                    b.HasData(
                        new
                        {
                            UnitId = 1,
                            Abbreviation = "MG",
                            AllowedInDropDown = false,
                            Name = "Milligrams"
                        },
                        new
                        {
                            UnitId = 2,
                            Abbreviation = "UG",
                            AllowedInDropDown = false,
                            Name = "Micrograms"
                        },
                        new
                        {
                            UnitId = 3,
                            Abbreviation = "G",
                            AllowedInDropDown = true,
                            Name = "Grams"
                        },
                        new
                        {
                            UnitId = 4,
                            Abbreviation = "IU",
                            AllowedInDropDown = false,
                            Name = "International Units"
                        },
                        new
                        {
                            UnitId = 5,
                            Abbreviation = "kJ",
                            AllowedInDropDown = false,
                            Name = "Kilojoules"
                        },
                        new
                        {
                            UnitId = 6,
                            Abbreviation = "KCAL",
                            AllowedInDropDown = false,
                            Name = "Calories"
                        },
                        new
                        {
                            UnitId = 7,
                            Abbreviation = "OZ",
                            AllowedInDropDown = true,
                            Name = "Ounces"
                        },
                        new
                        {
                            UnitId = 8,
                            Abbreviation = "C",
                            AllowedInDropDown = true,
                            Name = "Cups"
                        },
                        new
                        {
                            UnitId = 9,
                            Abbreviation = "LB",
                            AllowedInDropDown = true,
                            Name = "Pounds"
                        },
                        new
                        {
                            UnitId = 10,
                            Abbreviation = "TBSP",
                            AllowedInDropDown = true,
                            Name = "Tablespoons"
                        },
                        new
                        {
                            UnitId = 11,
                            Abbreviation = "TSP",
                            AllowedInDropDown = true,
                            Name = "Teaspoons"
                        },
                        new
                        {
                            UnitId = 12,
                            Abbreviation = "FL OZ",
                            AllowedInDropDown = true,
                            Name = "Fluid Ounces"
                        },
                        new
                        {
                            UnitId = 13,
                            Abbreviation = "PT",
                            AllowedInDropDown = true,
                            Name = "Pints"
                        },
                        new
                        {
                            UnitId = 14,
                            Abbreviation = "QT",
                            AllowedInDropDown = true,
                            Name = "Quarts"
                        },
                        new
                        {
                            UnitId = 15,
                            Abbreviation = "GAL",
                            AllowedInDropDown = true,
                            Name = "Gallons"
                        },
                        new
                        {
                            UnitId = 16,
                            Abbreviation = "W",
                            AllowedInDropDown = true,
                            Name = "Whole"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
