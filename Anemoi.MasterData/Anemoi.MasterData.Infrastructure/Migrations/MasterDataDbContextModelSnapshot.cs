﻿// <auto-generated />
using System;
using Anemoi.MasterData.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Anemoi.MasterData.Infrastructure.Migrations
{
    [DbContext(typeof(MasterDataDbContext))]
    partial class MasterDataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Anemoi.MasterData.Domain.Models.District", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<Guid?>("ProvinceId")
                        .HasColumnType("uuid");

                    b.Property<string>("SearchHint")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("Slug")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.HasIndex("Slug");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("Anemoi.MasterData.Domain.Models.Province", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<string>("SearchHint")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("Slug")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.HasKey("Id");

                    b.HasIndex("Slug");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("Anemoi.MasterData.Domain.Models.District", b =>
                {
                    b.HasOne("Anemoi.MasterData.Domain.Models.Province", "Province")
                        .WithMany("Districts")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Province");
                });

            modelBuilder.Entity("Anemoi.MasterData.Domain.Models.Province", b =>
                {
                    b.Navigation("Districts");
                });
#pragma warning restore 612, 618
        }
    }
}
