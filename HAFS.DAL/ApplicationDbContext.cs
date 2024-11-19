using Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserDb> UsersDb { get; set; }
    
    public DbSet<OrderDb> OrdersDb { get; set; }
    
    public DbSet<ProductDb> ProductsDb { get; set; }
    
    public DbSet<CategoryDb> CategoriesDb { get; set; }
    
    
    protected readonly IConfiguration Configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}

