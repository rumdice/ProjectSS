using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoreDB.DBLogApp;

public partial class DbLogAppContext : DbContext
{
    public DbLogAppContext(DbContextOptions<DbLogAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountEntity> AccountEntity { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AccountEntity>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => e.pid, "pk");

            entity.Property(e => e.aid).HasColumnType("bigint(20)");
            entity.Property(e => e.name).HasMaxLength(50);
            entity.Property(e => e.password).HasMaxLength(50);
            entity.Property(e => e.pid).HasColumnType("bigint(20)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
