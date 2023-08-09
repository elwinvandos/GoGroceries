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
    [Migration("20230808094728_AddTemplates")]
    partial class AddTemplates
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

                    b.Property<string>("ColorCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("color_code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryLists.GroceryList", b =>
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

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryLists.GroceryListProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.Property<Guid>("GroceryListId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("grocery_list_id");

                    b.Property<bool>("IsCheckedOff")
                        .HasColumnType("bit")
                        .HasColumnName("is_checked_off");

                    b.Property<string>("Measurement")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("measurement");

                    b.Property<int?>("MeasurementQuantity")
                        .HasColumnType("int")
                        .HasColumnName("measurement_quantity");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("product_id");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("pk_grocery_list_product");

                    b.HasIndex("GroceryListId")
                        .HasDatabaseName("ix_grocery_list_product_grocery_list_id");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_grocery_list_product_product_id");

                    b.ToTable("grocery_list_product", (string)null);
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryLists.Templates.GroceryListTemplate", b =>
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
                        .HasName("pk_grocery_list_templates");

                    b.ToTable("grocery_list_templates", (string)null);
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryLists.Templates.GroceryListTemplateProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.Property<Guid>("GroceryListTemplateId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("grocery_list_template_id");

                    b.Property<string>("Measurement")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("measurement");

                    b.Property<int?>("MeasurementQuantity")
                        .HasColumnType("int")
                        .HasColumnName("measurement_quantity");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("product_id");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("pk_grocery_list_template_product");

                    b.HasIndex("GroceryListTemplateId")
                        .HasDatabaseName("ix_grocery_list_template_product_grocery_list_template_id");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_grocery_list_template_product_product_id");

                    b.ToTable("grocery_list_template_product", (string)null);
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.Product", b =>
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
                        .HasName("pk_products");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryLists.GroceryListProduct", b =>
                {
                    b.HasOne("Elwin.GoGroceries.Domain.Models.GroceryLists.GroceryList", "GroceryList")
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

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryLists.Templates.GroceryListTemplateProduct", b =>
                {
                    b.HasOne("Elwin.GoGroceries.Domain.Models.GroceryLists.Templates.GroceryListTemplate", "GroceryListTemplate")
                        .WithMany("TemplateProducts")
                        .HasForeignKey("GroceryListTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_grocery_list_template_product_grocery_list_templates_grocery_list_template_id");

                    b.HasOne("Elwin.GoGroceries.Domain.Models.Product", "Product")
                        .WithMany("TemplateProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_grocery_list_template_product_products_product_id");

                    b.Navigation("GroceryListTemplate");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryLists.GroceryList", b =>
                {
                    b.Navigation("ListProducts");
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.GroceryLists.Templates.GroceryListTemplate", b =>
                {
                    b.Navigation("TemplateProducts");
                });

            modelBuilder.Entity("Elwin.GoGroceries.Domain.Models.Product", b =>
                {
                    b.Navigation("ListProducts");

                    b.Navigation("TemplateProducts");
                });
#pragma warning restore 612, 618
        }
    }
}