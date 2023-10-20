using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Models
{
    public class E_ecommerceContext:IdentityDbContext<ApplicationUser>
    {
        public E_ecommerceContext(DbContextOptions<E_ecommerceContext> options) : base(options)
        {
                
        }

        public DbSet<Product> products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Product>()
                .HasIndex(p => p.ProductCode)
                .IsUnique();

            builder.Entity<ApplicationUser>()
                .HasIndex(a=>a.UserName)
                .IsUnique();


            builder.Entity<ApplicationUser>()
                .HasIndex(a => a.Email)
                .IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
