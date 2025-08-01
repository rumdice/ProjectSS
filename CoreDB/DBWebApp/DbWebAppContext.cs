using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace CoreDB.DBWebApp;

public partial class DbWebAppContext : DbContext
{
    public DbWebAppContext()
    {
    }

    public DbWebAppContext(DbContextOptions<DbWebAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }

    public virtual DbSet<ItemSimpleEntity> ItemSimpleEntities { get; set; }

    public virtual DbSet<UserEntity> UserEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=13306;database=db_WebApp;user=root;password=pass", Microsoft.EntityFrameworkCore.ServerVersion.Parse("11.5.2-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<EfmigrationsHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__EFMigrationsHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<ItemSimpleEntity>(entity =>
        {
            entity.HasKey(e => e.ItemUid).HasName("PRIMARY");

            entity.ToTable("ItemSimpleEntity");

            entity.Property(e => e.ItemUid).HasColumnType("bigint(11)");
            entity.Property(e => e.Grade).HasColumnType("int(11)");
            entity.Property(e => e.ItemTid).HasColumnType("bigint(11)");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.UserUid).HasColumnType("bigint(11)");
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.UserUid).HasName("PRIMARY");

            entity.ToTable("UserEntity");

            entity.Property(e => e.UserUid).HasColumnType("bigint(11)");
            entity.Property(e => e.Level).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
