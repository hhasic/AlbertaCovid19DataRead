using Microsoft.EntityFrameworkCore;

namespace AlbertaCovid19DataRead
{
    public class DataContext : DbContext
    {
        public DataContext() : base() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Area> Area { get; set; }
        public DbSet<AreaCovidInfo> AreaCovidInfo { get; set; }
        public DbSet<LabTesting> LabTesting { get; set; }
        public DbSet<Cases> Cases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-NCA8CAVU\LOCALHOST;Database=AlbertaCovid19Data;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {                   
            modelBuilder.Entity<Area>()
                .HasMany(e => e.AreaCovidData)
                .WithOne(e => e.Area)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
