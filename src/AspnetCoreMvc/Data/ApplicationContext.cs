using AspnetCoreMvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvc.Data;

public class ApplicationContext : IdentityDbContext
{
    public DbSet<Produto> Produtos { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
}