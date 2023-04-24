﻿// <auto-generated />
using System;
using Elwin.GoGroceries.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Elwin.GoGroceries.Infrastructure.Migrations
{
    [DbContext(typeof(GroceriesContext))]
    partial class GroceriesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.Property<Guid?>("GroceryListId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("grocery_list_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_grocery_items");

                    b.HasIndex("GroceryListId")
                        .HasDatabaseName("ix_grocery_items_grocery_list_id");

                    b.ToTable("grocery_items", (string)null);
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_grocery_lists");

                    b.ToTable("grocery_lists", (string)null);
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryItem", b =>
                {
                    b.HasOne("Elwin.GoGroceries.Domain.Models.GroceryList", null)
                        .WithMany("GroceryItems")
                        .HasForeignKey("GroceryListId")
                        .HasConstraintName("fk_grocery_items_grocery_lists_grocery_list_id");
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryList", b =>
                {
                    b.Navigation("GroceryItems");
                });
#pragma warning restore 612, 618
        }
    }
}