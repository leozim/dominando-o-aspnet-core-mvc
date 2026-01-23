using AspnetCoreMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvc.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
}