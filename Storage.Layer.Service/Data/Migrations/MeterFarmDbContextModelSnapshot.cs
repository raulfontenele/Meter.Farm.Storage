﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Storage.Layer.Service.Data;

#nullable disable

namespace Storage.Layer.Service.Data.Migrations
{
    [DbContext(typeof(MeterFarmDbContext))]
    partial class MeterFarmDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Meter.Farm.DTO.Repository.CommandStorageObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CommandName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DatetimeReceive")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DatetimeRequest")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MeterUUID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("RxCommand")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("TxCommand")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.HasKey("Id");

                    b.ToTable("CommandStorageObjects", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
