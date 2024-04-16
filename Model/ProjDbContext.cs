using Microsoft.EntityFrameworkCore;

namespace Project.Model
{
    public class ProjDbContext: DbContext
    {
        public ProjDbContext(DbContextOptions<ProjDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Cart> Carts { get; set; }
        
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Order_Item> Order_Items { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.Role)
                .HasDefaultValue("Customer");
        }
    }
}
