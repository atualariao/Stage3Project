﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using S3E1.Data;

#nullable disable

namespace S3E1.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20221111011121_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("S3E1.Entities.CartItemEntity", b =>
                {
                    b.Property<Guid>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("ItemPrice")
                        .HasColumnType("float");

                    b.Property<Guid?>("OrderEntityOrderID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ItemID");

                    b.HasIndex("OrderEntityOrderID");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("S3E1.Entities.OrderEntity", b =>
                {
                    b.Property<Guid>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserRefID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderID");

                    b.HasIndex("UserRefID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("S3E1.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("S3E1.Entities.CartItemEntity", b =>
                {
                    b.HasOne("S3E1.Entities.OrderEntity", null)
                        .WithMany("CartItems")
                        .HasForeignKey("OrderEntityOrderID");
                });

            modelBuilder.Entity("S3E1.Entities.OrderEntity", b =>
                {
                    b.HasOne("S3E1.Entities.UserEntity", "UserModel")
                        .WithMany("Orders")
                        .HasForeignKey("UserRefID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserModel");
                });

            modelBuilder.Entity("S3E1.Entities.OrderEntity", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("S3E1.Entities.UserEntity", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
