﻿// <auto-generated />
using System;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    partial class OrderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Data.Model.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("Data.Model.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique()
                        .HasFilter("[OrderId] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("invoices");
                });

            modelBuilder.Entity("Data.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("int");

                    b.Property<int?>("PromotionId")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("PromotionId");

                    b.HasIndex("UserId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("Data.Model.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameProduct")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("orderDetails");
                });

            modelBuilder.Entity("Data.Model.PaymentMethod", b =>
                {
                    b.Property<int>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentMethodId"), 1L, 1);

                    b.Property<string>("MethodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("paymentMethods");
                });

            modelBuilder.Entity("Data.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaProduct")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Material")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NameProduct")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Data.Model.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("productCategories");
                });

            modelBuilder.Entity("Data.Model.ProductSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("SizeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SizeId");

                    b.ToTable("productSizes");
                });

            modelBuilder.Entity("Data.Model.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double?>("DiscountAmount")
                        .HasColumnType("float");

                    b.Property<double?>("DiscountPercent")
                        .HasColumnType("float");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("promotions");
                });

            modelBuilder.Entity("Data.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Administrator role",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Customer role",
                            Name = "Customer"
                        });
                });

            modelBuilder.Entity("Data.Model.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("sizes");
                });

            modelBuilder.Entity("Data.Model.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("suppliers");
                });

            modelBuilder.Entity("Data.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Data.Model.Invoice", b =>
                {
                    b.HasOne("Data.Model.Order", "Order")
                        .WithOne("Invoice")
                        .HasForeignKey("Data.Model.Invoice", "OrderId");

                    b.HasOne("Data.Model.User", "User")
                        .WithMany("invoices")
                        .HasForeignKey("UserId");

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Model.Order", b =>
                {
                    b.HasOne("Data.Model.PaymentMethod", "paymentMethods")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentMethodId");

                    b.HasOne("Data.Model.Promotion", "Promotion")
                        .WithMany("orders")
                        .HasForeignKey("PromotionId");

                    b.HasOne("Data.Model.User", "User")
                        .WithMany("orders")
                        .HasForeignKey("UserId");

                    b.Navigation("Promotion");

                    b.Navigation("User");

                    b.Navigation("paymentMethods");
                });

            modelBuilder.Entity("Data.Model.OrderDetail", b =>
                {
                    b.HasOne("Data.Model.Order", "Order")
                        .WithMany("orderDetails")
                        .HasForeignKey("OrderId");

                    b.HasOne("Data.Model.Product", "Product")
                        .WithMany("orderDetails")
                        .HasForeignKey("ProductId");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Data.Model.Product", b =>
                {
                    b.HasOne("Data.Model.Supplier", "Suppliers")
                        .WithMany("products")
                        .HasForeignKey("SupplierId");

                    b.Navigation("Suppliers");
                });

            modelBuilder.Entity("Data.Model.ProductCategory", b =>
                {
                    b.HasOne("Data.Model.Category", "Category")
                        .WithMany("productCategories")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Data.Model.Product", "Product")
                        .WithMany("productCategories")
                        .HasForeignKey("ProductId");

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Data.Model.ProductSize", b =>
                {
                    b.HasOne("Data.Model.Product", "Product")
                        .WithMany("ProductSizes")
                        .HasForeignKey("ProductId");

                    b.HasOne("Data.Model.Size", "Size")
                        .WithMany("ProductSizes")
                        .HasForeignKey("SizeId");

                    b.Navigation("Product");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Data.Model.User", b =>
                {
                    b.HasOne("Data.Model.Role", "Role")
                        .WithMany("users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Data.Model.Category", b =>
                {
                    b.Navigation("productCategories");
                });

            modelBuilder.Entity("Data.Model.Order", b =>
                {
                    b.Navigation("Invoice");

                    b.Navigation("orderDetails");
                });

            modelBuilder.Entity("Data.Model.PaymentMethod", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Data.Model.Product", b =>
                {
                    b.Navigation("ProductSizes");

                    b.Navigation("orderDetails");

                    b.Navigation("productCategories");
                });

            modelBuilder.Entity("Data.Model.Promotion", b =>
                {
                    b.Navigation("orders");
                });

            modelBuilder.Entity("Data.Model.Role", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("Data.Model.Size", b =>
                {
                    b.Navigation("ProductSizes");
                });

            modelBuilder.Entity("Data.Model.Supplier", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("Data.Model.User", b =>
                {
                    b.Navigation("invoices");

                    b.Navigation("orders");
                });
#pragma warning restore 612, 618
        }
    }
}
