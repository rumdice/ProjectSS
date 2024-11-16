﻿// <auto-generated />
using System;
using CoreLibrary.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoreLibrary.Migrations
{
    [DbContext(typeof(DbWebAppContext))]
    partial class DbWebAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("CoreLibrary.Database.ItemSimpleEntity", b =>
                {
                    b.Property<long>("ItemUid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(11)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("ItemUid"));

                    b.Property<int?>("Grade")
                        .HasColumnType("int(11)");

                    b.Property<long?>("ItemTid")
                        .HasColumnType("bigint(11)");

                    b.Property<string>("Name")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<long?>("UserUid")
                        .HasColumnType("bigint(11)");

                    b.HasKey("ItemUid")
                        .HasName("PRIMARY");

                    b.ToTable("ItemSimpleEntity", (string)null);
                });

            modelBuilder.Entity("CoreLibrary.Database.UserEntity", b =>
                {
                    b.Property<long>("UserUid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(11)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("UserUid"));

                    b.Property<int?>("Level")
                        .HasColumnType("int(11)");

                    b.Property<string>("Name")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("UserUid")
                        .HasName("PRIMARY");

                    b.ToTable("UserEntity", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}