﻿// <auto-generated />
using System;
using DocumentationGenerator.Service.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DocumentationGenerator.Service.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240129172630_UpdateIdFormat")]
    partial class UpdateIdFormat
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.15");

            modelBuilder.Entity("DocumentationGenerator.Service.Entities.FileEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("CREATE_AT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasColumnName("NAME");

                    b.Property<string>("SavedPath")
                        .HasColumnType("TEXT")
                        .HasColumnName("SAVED_PATH");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("STATUS");

                    b.HasKey("Id");

                    b.ToTable("FILE");
                });
#pragma warning restore 612, 618
        }
    }
}
