using System;
using Microsoft.EntityFrameworkCore;

namespace CoreDB.DBWebApp
{
    public partial class DbWebAppContext : DbContext
    {
        public DbWebAppContext()
        {
        }

        public DbWebAppContext(DbContextOptions<DbWebAppContext> options)
            : base(options)
        {
        }

        // ✅ 복수형 컨벤션
        public virtual DbSet<UserEntity> UserEntities { get; set; }

        // ✅ 연결 문자열 명시 + MariaDB 11.8.2
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#warning "보안을 위해 연결 문자열을 외부 구성(appsettings.json 등)으로 옮기세요"
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=localhost;port=13306;database=db_WebApp;user=root;password=pass;",
                    new MySqlServerVersion(new Version(11, 8, 2))
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.uid).HasName("PRIMARY");

                entity.ToTable("UserEntity");

                entity.Property(e => e.uid).HasColumnType("bigint(11)");
                entity.Property(e => e.level).HasColumnType("int(11)");
                entity.Property(e => e.name).HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}