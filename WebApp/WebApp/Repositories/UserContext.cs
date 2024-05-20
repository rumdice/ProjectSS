using CoreLibrary;
using Microsoft.EntityFrameworkCore;
using WepApp.DtoModels;


namespace WebApp.Reposotory;

public class UserContext : DbContext
{
    public DbSet<UserDto> Users { get; set; }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    
    }

}