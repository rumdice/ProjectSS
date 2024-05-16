
using CoreLibrary;
using Microsoft.EntityFrameworkCore;
using WepApp.Models;

namespace WebApp.Reposotory;

public class ItemContext : DbContext
{
    public ItemContext(DbContextOptions<ItemContext> options)
        : base(options)
    {
    }

    public DbSet<Item> TodoItems { get; set; } = null!;
}