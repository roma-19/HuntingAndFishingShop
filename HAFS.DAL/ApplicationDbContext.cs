using Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<UserDb> UsersDb { get; set; }
    
    public DbSet<OrderDb> OrdersDb { get; set; }
    
    public DbSet<ProductDb> ProductsDb { get; set; }
    
    public DbSet<CategoryDb> CategoriesDb { get; set; }
    
    public DbSet<CartItemDb> CartItemsDb { get; set; }
    
    protected readonly IConfiguration Configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}

