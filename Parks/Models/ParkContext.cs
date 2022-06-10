using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Park.Models
{
    public class ParkContext : IdentityDbContext<ApplicationUser>
    {
      public DbSet<NationalPark> NationalParks { get; set; }
      public DbSet<StatePark> StateParks { get; set; }
      
      public ParkContext(DbContextOptions<ParkContext> options) : base(options) {  }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
        optionsBuilder.UseLazyLoadingProxies();
      }
    }
}