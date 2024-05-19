
using CoreLibrary;
using Microsoft.EntityFrameworkCore;
using WepApp.Models;

namespace WebApp.Reposotory;


public class ItemContext : DbContext
{
    public DbSet<ItemDto> TodoItems { get; set; } = null!;
    
    public ItemContext(DbContextOptions<ItemContext> options)
        : base(options)
    {
    }

}