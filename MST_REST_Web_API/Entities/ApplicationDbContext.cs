using Microsoft.EntityFrameworkCore;

namespace MST_REST_Web_API.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
        public DbSet<EndpointType> EndpointTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(e => e.Products);

            //modelBuilder.Entity<Script>()
            //.HasMany(a => a.Endpoints)
            //.WithRequired()
            //.WillCascadeOnDelete(true);

        }
    }
}
