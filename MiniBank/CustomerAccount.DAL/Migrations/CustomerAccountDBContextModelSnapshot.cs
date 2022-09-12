﻿// <auto-generated />
using System;
using CustomerAccount.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.CustomerAccountDAL.ValueConversion;

#nullable disable

namespace CustomerAccount.DAL.Migrations
{
    [DbContext(typeof(CustomerAccountDBContext))]
    partial class CustomerAccountDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CustomerAccount.DAL.Entities.AccountData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OpenDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("AccountDatas");
                });

            modelBuilder.Entity("CustomerAccount.DAL.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CustomerAccount.DAL.Entities.OperationData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<bool>("IsCredit")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OperationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionAmount")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("CustomerAccount.DAL.Entities.AccountData", b =>
                {
                    b.HasOne("CustomerAccount.DAL.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CustomerAccount.DAL.Entities.OperationData", b =>
                {
                    b.HasOne("CustomerAccount.DAL.Entities.AccountData", "AccountData")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountData");
                });
#pragma warning restore 612, 618
        }
    }
}
