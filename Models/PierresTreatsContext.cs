using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PierresTreats.Models
{
    public class PierresTreatsContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Treat> Treats { get; set; }
    public DbSet<Flavor> Flavors { get; set; }
    public DbSet<TreatFlavor> TreatFlavor { get; set; }

    public PierresTreatsContext(DbContextOptions options) : base(options) { }
  }
}