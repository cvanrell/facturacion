﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.Billing.DataAccessCore.Migrations
{
    [DbContext(typeof(WISDB))]
    [Migration("20190705171749_Migration4")]
    partial class Migration4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WIS.Billing.DataAccessCore.Database.T_GRID_DEFAULT_CONFIG", b =>
                {
                    b.Property<string>("CD_APLICACION")
                        .HasMaxLength(30);

                    b.Property<string>("CD_BLOQUE")
                        .HasMaxLength(30);

                    b.Property<string>("NM_DATAFIELD")
                        .HasMaxLength(30);

                    b.Property<string>("DS_COLUMNA")
                        .HasMaxLength(100);

                    b.Property<string>("DS_DATA_FORMAT_STRING")
                        .HasMaxLength(30);

                    b.Property<string>("FL_VISIBLE")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<short?>("NU_ORDEN_VISUAL");

                    b.Property<int?>("RESOURCEID");

                    b.Property<string>("VL_ALINEACION")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<string>("VL_LINK")
                        .HasMaxLength(500);

                    b.Property<short?>("VL_POSICION_FIJADO");

                    b.Property<string>("VL_TYPE")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<decimal?>("VL_WIDTH");

                    b.HasKey("CD_APLICACION", "CD_BLOQUE", "NM_DATAFIELD");

                    b.ToTable("T_GRID_DEFAULT_CONFIG");
                });

            modelBuilder.Entity("WIS.Billing.DataAccessCore.Database.T_GRID_USER_CONFIG", b =>
                {
                    b.Property<string>("CD_APLICACION")
                        .HasMaxLength(30);

                    b.Property<string>("CD_BLOQUE")
                        .HasMaxLength(30);

                    b.Property<string>("NM_DATAFIELD")
                        .HasMaxLength(30);

                    b.Property<string>("DS_COLUMNA")
                        .HasMaxLength(100);

                    b.Property<string>("DS_DATA_FORMAT_STRING")
                        .HasMaxLength(30);

                    b.Property<string>("FL_VISIBLE")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<short?>("NU_ORDEN_VISUAL");

                    b.Property<int?>("RESOURCEID");

                    b.Property<int>("USERID");

                    b.Property<string>("VL_ALINEACION")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<string>("VL_LINK")
                        .HasMaxLength(500);

                    b.Property<short?>("VL_POSICION_FIJADO");

                    b.Property<string>("VL_TYPE")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<decimal?>("VL_WIDTH");

                    b.HasKey("CD_APLICACION", "CD_BLOQUE", "NM_DATAFIELD");

                    b.HasAlternateKey("USERID");

                    b.ToTable("T_GRID_USER_CONFIG");
                });

            modelBuilder.Entity("WIS.Billing.EntitiesCore.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("RUT");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("WIS.Billing.EntitiesCore.Development", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid?>("ClientId");

                    b.Property<Guid?>("RateId");

                    b.Property<string>("TicketId");

                    b.Property<decimal>("TotalHours");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("RateId");

                    b.ToTable("Developments");
                });

            modelBuilder.Entity("WIS.Billing.EntitiesCore.Fee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("Description");

                    b.Property<decimal>("IVA");

                    b.Property<int>("Month");

                    b.Property<Guid?>("ProjectId");

                    b.Property<decimal>("Total");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Fee");
                });

            modelBuilder.Entity("WIS.Billing.EntitiesCore.HourRate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdjustmentPeriodicity");

                    b.Property<int>("Amount");

                    b.Property<Guid?>("ClientId");

                    b.Property<int>("Currency");

                    b.Property<string>("Description");

                    b.Property<decimal>("Rate");

                    b.Property<decimal>("SpecialDiscount");

                    b.Property<int>("StartingMonth");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("HourRates");
                });

            modelBuilder.Entity("WIS.Billing.EntitiesCore.Maintenance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid?>("ClientId");

                    b.Property<int>("Currency");

                    b.Property<string>("Description");

                    b.Property<decimal>("IVA");

                    b.Property<int>("Periodicity");

                    b.Property<decimal>("Total");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Maintenances");
                });

            modelBuilder.Entity("WIS.Billing.EntitiesCore.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid?>("ClientId");

                    b.Property<int>("Currency");

                    b.Property<string>("Description");

                    b.Property<decimal>("IVA");

                    b.Property<int>("Installments");

                    b.Property<decimal>("Total");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("WIS.Billing.EntitiesCore.Development", b =>
                {
                    b.HasOne("WIS.Billing.EntitiesCore.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("WIS.Billing.EntitiesCore.HourRate", "Rate")
                        .WithMany()
                        .HasForeignKey("RateId");
                });

            modelBuilder.Entity("WIS.Billing.EntitiesCore.Fee", b =>
                {
                    b.HasOne("WIS.Billing.EntitiesCore.Project", "Project")
                        .WithMany("Fees")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("WIS.Billing.EntitiesCore.HourRate", b =>
                {
                    b.HasOne("WIS.Billing.EntitiesCore.Client", "Client")
                        .WithMany("HourRates")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("WIS.Billing.EntitiesCore.Maintenance", b =>
                {
                    b.HasOne("WIS.Billing.EntitiesCore.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("WIS.Billing.EntitiesCore.Project", b =>
                {
                    b.HasOne("WIS.Billing.EntitiesCore.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");
                });
#pragma warning restore 612, 618
        }
    }
}
