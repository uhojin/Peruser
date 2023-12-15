﻿// <auto-generated />
using System;
using API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20231214094130_ListingOwnerId")]
    partial class ListingOwnerId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("API.Models.Entities.Listing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PostingDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Listings");
                });

            modelBuilder.Entity("API.Models.Entities.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("BuyerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ListingId")
                        .HasColumnType("TEXT");

                    b.Property<int>("OfferAmount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OfferDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("ListingId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("API.Models.Entities.User", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Currency")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Priviledged")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Models.Entities.Listing", b =>
                {
                    b.HasOne("API.Models.Entities.User", null)
                        .WithMany("Listings")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("API.Models.Entities.Offer", b =>
                {
                    b.HasOne("API.Models.Entities.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId");

                    b.HasOne("API.Models.Entities.Listing", "Listing")
                        .WithMany("Offers")
                        .HasForeignKey("ListingId");

                    b.Navigation("Buyer");

                    b.Navigation("Listing");
                });

            modelBuilder.Entity("API.Models.Entities.Listing", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("API.Models.Entities.User", b =>
                {
                    b.Navigation("Listings");
                });
#pragma warning restore 612, 618
        }
    }
}
