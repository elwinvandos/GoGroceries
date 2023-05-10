﻿// <auto-generated />
using System;
using Elwin.GoGroceries.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Elwin.GoGroceries.Infrastructure.Migrations
{
    [DbContext(typeof(GroceriesContext))]
    [Migration("20230510103336_ProductCategoryToRequired")]
    partial class ProductCategoryToRequired
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryListProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("GroceryListId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("grocery_list_id");

                    b.Property<bool>("IsCheckedOff")
                        .HasColumnType("bit")
                        .HasColumnName("is_checked_off");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("product_id");

                    b.HasKey("Id")
                        .HasName("pk_grocery_list_product");

                    b.HasIndex("GroceryListId")
                        .HasDatabaseName("ix_grocery_list_product_grocery_list_id");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_grocery_list_product_product_id");

                    b.ToTable("grocery_list_product", (string)null);
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<int?>("Weight")
                        .HasColumnType("int")
                        .HasColumnName("weight");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryListProduct", b =>
                {
                    b.HasOne("Elwin.GoGroceries.Domain.Models.GroceryList", "GroceryList")
                        .WithMany("ListProducts")
                        .HasForeignKey("GroceryListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_grocery_list_product_grocery_lists_grocery_list_id");

                    b.HasOne("Elwin.GoGroceries.Domain.Models.Product", "Product")
                        .WithMany("ListProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_grocery_list_product_products_product_id");

                    b.Navigation("GroceryList");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryList", b =>
                {
                    b.Navigation("ListProducts");
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.Product", b =>
                {
                    b.Navigation("ListProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
