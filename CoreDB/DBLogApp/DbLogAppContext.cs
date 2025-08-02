using System;
using Microsoft.EntityFrameworkCore;

namespace CoreDB.DBLogApp
{
    public partial class DbLogAppContext : DbContext
    {
        public DbLogAppContext()
        {
        }

        public DbLogAppContext(DbContextOptions<DbLogAppContext> options)
            : base(options)
        {
        }

        // ✅ 복수형으로 변경 추천
        public virtual DbSet<AccountEntity> AccountEntities { get; set; }

        // ✅ 연결 문자열 명시
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#warning "보안을 위해 연결 문자열을 외부 구성(appsettings.json 등)으로 옮기세요"
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=localhost;port=13306;database=db_LogApp;user=root;password=pass;",
                    new MySqlServerVersion(new Version(11, 8, 2)) // 실제 MariaDB 버전에 맞게 수정 가능
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_uca1400_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<AccountEntity>(entity =>
            {
                entity.HasKey(e => e.pid).HasName("PRIMARY");
                entity.HasIndex(e => e.name, "idx_name");

                entity.Property(e => e.pid).HasColumnType("bigint(20)");
                entity.Property(e => e.name).HasMaxLength(50);
                entity.Property(e => e.password).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}