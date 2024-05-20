
using CoreLibrary;
using Microsoft.EntityFrameworkCore;
using WepApp.DtoModels;

namespace WebApp.Reposotory;


public class ItemContext : DbContext
{
    //public DbSet<ItemDto> Items { get; set; } = null!;

    public DbSet<ItemSimpleInfoDto> ItemSimpleInfo { get; set; }
    
    public ItemContext(DbContextOptions<ItemContext> options)
        : base(options)
    {
    }

}