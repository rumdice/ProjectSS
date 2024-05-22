using Microsoft.EntityFrameworkCore;
using WepApp.DtoModels;

namespace WebApp.Database;


public class ItemContext : DbContext
{
    
    public DbSet<ItemSimpleEntity> ItemSimpleEntity { get; set; }
    
    public ItemContext(DbContextOptions<ItemContext> options)
        : base(options)
    {
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseMySql("Server=localhost;Database=mydatabase;User=myuser;Password=mypassword;",
    //         new MySqlServerVersion(new Version(8, 0, 21)));
    // }
}