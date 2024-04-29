﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderWebAPI.Data;

#nullable disable

namespace OrderWebAPI.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    partial class OrderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OrderWebAPI.Models.Domains.ORDER", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("Customer_Id")
                        .HasColumnType("int")
                        .HasColumnName("customer_Id");

                    b.Property<DateTime?>("OrderOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("orderOn");

                    b.Property<int?>("Product_Id")
                        .HasColumnType("int")
                        .HasColumnName("product_Id");

                    b.Property<string>("Status")
                        .HasColumnType("longtext")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("ORDERS");
                });
#pragma warning restore 612, 618
        }
    }
}
