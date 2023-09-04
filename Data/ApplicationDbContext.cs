using Microsoft.EntityFrameworkCore;
using AuthProject.Models;

namespace AuthProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        // public DbSet<Category> Categories { get; set; }
       
        public DbSet<User> Users { get; set; }    
        // public DbSet<Customer> Customer { get; set; }
    // public DbSet<CustomerAddresses> customerAddresses { get; set; }
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<CustomerAddresses>()
    //     .HasOne(_ => _.Customer)
    //     .WithMany(a => a.CustomerAddresses)
    //     .HasForeignKey(p => p.CustomerId);
    // }   
    }
}
