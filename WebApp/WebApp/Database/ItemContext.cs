using Microsoft.EntityFrameworkCore;

namespace WebApp.Database;


public class ItemContext : DbContext
{
    public DbSet<ItemSimpleEntity> ItemSimpleEntity { get; set; }
    
    public ItemContext(DbContextOptions<ItemContext> options)
        : base(options)
    {
    }
}

