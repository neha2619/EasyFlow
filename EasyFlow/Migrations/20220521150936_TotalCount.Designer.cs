﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyFlow.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20220521150936_TotalCount")]
    partial class TotalCount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Entities.Models.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AdminId");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pass")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8a6ea087-cbc8-4699-92ce-13056ae550fe"),
                            Email = "Samraiden@gmail.com",
                            Mobile = "8976543209",
                            Name = "Sam Raiden",
                            Pass = "hshskhsk"
                        });
                });

            modelBuilder.Entity("Entities.Models.AdminCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedOn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vacancy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkerType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("AdminCompanyies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a3347540-c60d-45f4-8ade-af0a189397a3"),
                            CompanyId = new Guid("2a5ddaf5-d15b-4976-93a1-41e8f79aeccc"),
                            Location = "sector o",
                            Vacancy = "5",
                            WorkerType = "Plumber"
                        },
                        new
                        {
                            Id = new Guid("76c00047-4526-48a9-9947-ea2be4133e64"),
                            CompanyId = new Guid("c86111fd-8827-4435-a601-404f9a213c23"),
                            Location = "sector p",
                            Vacancy = "7",
                            WorkerType = "Carpenter"
                        });
                });

            modelBuilder.Entity("Entities.Models.AdminWorker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("CreatedOn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WorkerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WorkerType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("WorkerId");

                    b.ToTable("AdminWorkers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2d1abe8a-a2df-4394-a7c2-be8dad068519"),
                            Location = "Hazaratganj",
                            WorkerId = new Guid("76ed4298-c549-465b-af8f-a80abae08616"),
                            WorkerType = "Carpenter"
                        });
                });

            modelBuilder.Entity("Entities.Models.CompanyReq", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedOn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WorkerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("CompanyReq");
                });

            modelBuilder.Entity("Entities.Models.OTPs", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("recipientEmail")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("timestamp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("OTPs");
                });

            modelBuilder.Entity("Entities.Models.PreviousWorker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("PreviousWorker");
                });

            modelBuilder.Entity("Entities.Models.Timestamps", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("RecipientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TimeStamp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Timestamps");
                });

            modelBuilder.Entity("Entities.Models.TotalCounts", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("count")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("TotalCounts");
                });

            modelBuilder.Entity("Entities.Models.Worker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("WorkerId");

                    b.Property<string>("CreatedOn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KYCStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationPreference")
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<int>("Ratings")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedOn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkerAadhar")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("WorkerMail")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("WorkerMobile")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("WorkerName")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("WorkerPass")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("WorkerType")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<Guid?>("companyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("companyId");

                    b.ToTable("Workers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("46a32c93-1663-447f-8801-db7db9c42719"),
                            CreatedOn = "24.04.2022",
                            KYCStatus = "Yes",
                            LocationPreference = "Alambag,charbag",
                            Ratings = 0,
                            UpdatedOn = "12.05.2022",
                            WorkerAadhar = "123456789012",
                            WorkerMail = "Samraiden@gmail.com",
                            WorkerMobile = "8976543209",
                            WorkerName = "Sam Raiden",
                            WorkerPass = "hjhfdjgjgg",
                            WorkerType = "Plumber"
                        },
                        new
                        {
                            Id = new Guid("76ed4298-c549-465b-af8f-a80abae08616"),
                            CreatedOn = "21.04.2022",
                            KYCStatus = "No",
                            LocationPreference = "Hazaratganj,Aashiyana",
                            Ratings = 0,
                            UpdatedOn = "11.05.2022",
                            WorkerAadhar = "120000789012",
                            WorkerMail = "Ronraiden@gmail.com",
                            WorkerMobile = "8976786209",
                            WorkerName = "Ron Raiden",
                            WorkerPass = "hjhfdjsdsdgjgg",
                            WorkerType = "Carpenter"
                        });
                });

            modelBuilder.Entity("Entities.Models.WorkerReq", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("CreatedOn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WorkerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WorkerType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkerReq");
                });

            modelBuilder.Entity("Entities.Models.company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CompanyId");

                    b.Property<string>("CompanyArea")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CompanyCin")
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("CompanyDistrict")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CompanyGstin")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("CompanyMail")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CompanyMobile")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("CompanyPass")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CompanyState")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CompanySubArea")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CompanyType")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("CreatedOn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KYCStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiteLocation")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("UpdatedOn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkerNumber")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2a5ddaf5-d15b-4976-93a1-41e8f79aeccc"),
                            CompanyArea = "Ashiyana",
                            CompanyCin = "123456789012345678901",
                            CompanyDistrict = "Lucknow",
                            CompanyGstin = "123456789012345",
                            CompanyMail = "ITsolution@gmail.com",
                            CompanyMobile = "8318692776",
                            CompanyName = "IT_Solutions Ltd",
                            CompanyPass = "fsdgvdsg",
                            CompanyState = "Uttar Pradesh",
                            CompanySubArea = "Sector o",
                            CompanyType = "Hotel",
                            CreatedOn = "24.04.2022",
                            KYCStatus = "Yes",
                            SiteLocation = "sector o, sector p, sector d,sector f",
                            UpdatedOn = "12.05.2022",
                            WorkerNumber = "90"
                        },
                        new
                        {
                            Id = new Guid("c86111fd-8827-4435-a601-404f9a213c23"),
                            CompanyArea = "HHazratganj",
                            CompanyCin = "123456789012345672341",
                            CompanyDistrict = "Lucknow",
                            CompanyGstin = "123456788792345",
                            CompanyMail = "Adminsolution@gmail.com",
                            CompanyMobile = "8318692776",
                            CompanyName = "Admin_Solutions Ltd",
                            CompanyPass = "fsdedgvdsg",
                            CompanyState = "Uttar Pradesh",
                            CompanySubArea = "Rana Pratap Marg",
                            CompanyType = "Construction",
                            CreatedOn = "14.04.2022",
                            KYCStatus = "No",
                            SiteLocation = "Rana Pratap Marg, sector p, sector d,sector f",
                            UpdatedOn = "2.05.2022",
                            WorkerNumber = "90"
                        });
                });

            modelBuilder.Entity("Entities.Models.AdminCompany", b =>
                {
                    b.HasOne("Entities.Models.company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Entities.Models.AdminWorker", b =>
                {
                    b.HasOne("Entities.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Entities.Models.Worker", b =>
                {
                    b.HasOne("Entities.Models.company", null)
                        .WithMany("Worker")
                        .HasForeignKey("companyId");
                });

            modelBuilder.Entity("Entities.Models.company", b =>
                {
                    b.Navigation("Worker");
                });
#pragma warning restore 612, 618
        }
    }
}
