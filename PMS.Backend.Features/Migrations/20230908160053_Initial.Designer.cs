﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PMS.Backend.Features.Infrastructure;

#nullable disable

namespace PMS.Backend.Features.Migrations
{
    [DbContext(typeof(PmsContext))]
    [Migration("20230908160053_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.6.23329.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PMS.Backend.Features.Agency.Entities.Agency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CommissionMethod")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Agencies");
                });

            modelBuilder.Entity("PMS.Backend.Features.Agency.Entities.AgencyContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AgencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("AgencyId");

                    b.ToTable("AgencyContacts");
                });

            modelBuilder.Entity("PMS.Backend.Features.Agency.Entities.Agency", b =>
                {
                    b.OwnsOne("PMS.Backend.Features.Shared.ValueObjects.Commission", "DefaultCommission", b1 =>
                        {
                            b1.Property<Guid>("AgencyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Value")
                                .HasPrecision(5, 4)
                                .HasColumnType("decimal(5,4)");

                            b1.HasKey("AgencyId");

                            b1.ToTable("Agencies");

                            b1.WithOwner()
                                .HasForeignKey("AgencyId");
                        });

                    b.OwnsOne("PMS.Backend.Features.Shared.ValueObjects.Commission", "DefaultCommissionOnExtras", b1 =>
                        {
                            b1.Property<Guid>("AgencyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Value")
                                .HasPrecision(5, 4)
                                .HasColumnType("decimal(5,4)");

                            b1.HasKey("AgencyId");

                            b1.ToTable("Agencies");

                            b1.WithOwner()
                                .HasForeignKey("AgencyId");
                        });

                    b.OwnsOne("PMS.Backend.Features.Shared.ValueObjects.ContactDetails", "EmergencyContact", b1 =>
                        {
                            b1.Property<Guid>("AgencyId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("AgencyId");

                            b1.ToTable("Agencies");

                            b1.WithOwner()
                                .HasForeignKey("AgencyId");

                            b1.OwnsOne("PMS.Backend.Features.Shared.ValueObjects.Email", "Email", b2 =>
                                {
                                    b2.Property<Guid>("ContactDetailsAgencyId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(255)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(255)");

                                    b2.HasKey("ContactDetailsAgencyId");

                                    b2.ToTable("Agencies");

                                    b2.WithOwner()
                                        .HasForeignKey("ContactDetailsAgencyId");
                                });

                            b1.OwnsOne("PMS.Backend.Features.Shared.ValueObjects.Phone", "Phone", b2 =>
                                {
                                    b2.Property<Guid>("ContactDetailsAgencyId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(20)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(20)");

                                    b2.HasKey("ContactDetailsAgencyId");

                                    b2.ToTable("Agencies");

                                    b2.WithOwner()
                                        .HasForeignKey("ContactDetailsAgencyId");
                                });

                            b1.Navigation("Email");

                            b1.Navigation("Phone");
                        });

                    b.OwnsOne("PMS.Backend.Features.Shared.ValueObjects.RequiredString", "LegalName", b1 =>
                        {
                            b1.Property<Guid>("AgencyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)");

                            b1.HasKey("AgencyId");

                            b1.ToTable("Agencies");

                            b1.WithOwner()
                                .HasForeignKey("AgencyId");
                        });

                    b.Navigation("DefaultCommission");

                    b.Navigation("DefaultCommissionOnExtras");

                    b.Navigation("EmergencyContact")
                        .IsRequired();

                    b.Navigation("LegalName")
                        .IsRequired();
                });

            modelBuilder.Entity("PMS.Backend.Features.Agency.Entities.AgencyContact", b =>
                {
                    b.HasOne("PMS.Backend.Features.Agency.Entities.Agency", null)
                        .WithMany("Contacts")
                        .HasForeignKey("AgencyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne("PMS.Backend.Features.Shared.ValueObjects.ContactDetails", "ContactDetails", b1 =>
                        {
                            b1.Property<Guid>("AgencyContactId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("AgencyContactId");

                            b1.ToTable("AgencyContacts");

                            b1.WithOwner()
                                .HasForeignKey("AgencyContactId");

                            b1.OwnsOne("PMS.Backend.Features.Shared.ValueObjects.Email", "Email", b2 =>
                                {
                                    b2.Property<Guid>("ContactDetailsAgencyContactId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(255)
                                        .HasColumnType("nvarchar(255)");

                                    b2.HasKey("ContactDetailsAgencyContactId");

                                    b2.ToTable("AgencyContacts");

                                    b2.WithOwner()
                                        .HasForeignKey("ContactDetailsAgencyContactId");
                                });

                            b1.OwnsOne("PMS.Backend.Features.Shared.ValueObjects.Phone", "Phone", b2 =>
                                {
                                    b2.Property<Guid>("ContactDetailsAgencyContactId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(20)
                                        .HasColumnType("nvarchar(20)");

                                    b2.HasKey("ContactDetailsAgencyContactId");

                                    b2.ToTable("AgencyContacts");

                                    b2.WithOwner()
                                        .HasForeignKey("ContactDetailsAgencyContactId");
                                });

                            b1.Navigation("Email");

                            b1.Navigation("Phone");
                        });

                    b.OwnsOne("PMS.Backend.Features.Shared.ValueObjects.RequiredString", "Name", b1 =>
                        {
                            b1.Property<Guid>("AgencyContactId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)");

                            b1.HasKey("AgencyContactId");

                            b1.ToTable("AgencyContacts");

                            b1.WithOwner()
                                .HasForeignKey("AgencyContactId");
                        });

                    b.OwnsOne("PMS.Backend.Features.Shared.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("AgencyContactId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasMaxLength(100)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Country")
                                .HasMaxLength(100)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("State")
                                .HasMaxLength(100)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Street")
                                .HasMaxLength(255)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(255)");

                            b1.Property<string>("ZipCode")
                                .HasMaxLength(20)
                                .IsUnicode(false)
                                .HasColumnType("varchar(20)");

                            b1.HasKey("AgencyContactId");

                            b1.ToTable("AgencyContacts");

                            b1.WithOwner()
                                .HasForeignKey("AgencyContactId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("ContactDetails")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("PMS.Backend.Features.Agency.Entities.Agency", b =>
                {
                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
