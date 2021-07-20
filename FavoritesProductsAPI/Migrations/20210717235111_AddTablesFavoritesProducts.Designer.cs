﻿// <auto-generated />
using System;
using FavoritesProductsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FavoritesProductsAPI.Migrations
{
    [DbContext(typeof(FavoritesProductsContext))]
    [Migration("20210717235111_AddTablesFavoritesProducts")]
    partial class AddTablesFavoritesProducts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FavoritesProductsAPI.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("FavoritesProductsAPI.Models.FavoriteProduct", b =>
                {
                    b.Property<byte[]>("ProductId")
                        .HasColumnType("varbinary(16)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "ClientId");

                    b.HasIndex("ClientId");

                    b.ToTable("FavoritesProducts");
                });

            modelBuilder.Entity("FavoritesProductsAPI.Models.FavoriteProduct", b =>
                {
                    b.HasOne("FavoritesProductsAPI.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
