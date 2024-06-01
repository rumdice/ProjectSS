using Microsoft.EntityFrameworkCore;

namespace WebApp.Database;

public class UserContext : DbContext
{
    public DbSet<UserEntity> UserEntity { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

}