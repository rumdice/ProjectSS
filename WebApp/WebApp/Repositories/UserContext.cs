using CoreLibrary;
using Microsoft.EntityFrameworkCore;
using WepApp.Models;


namespace WebApp.Reposotory;

public class UserContext : DbContext
{
    public DbSet<UserDto> Users { get; set; } = null;

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    
    }

}