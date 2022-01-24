
using FindMyLocation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FindMyLocation.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<FourSqaureVenues> FourSqaureVenues{ get; set; }
        public DbSet<ImageModel> ImageModels{ get; set; }
        public DbSet<ModelFour> ModelFours { get; set; }
        // This constructor is used of runit testing
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=FindLocationData");
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder
            //    .UseSqlServer("Data Source=(localdb)\\MSQLLocalDB; Initial Catalog=FindLocationData");
            //}

        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
