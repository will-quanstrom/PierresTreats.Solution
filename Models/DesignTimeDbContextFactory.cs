using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PierresTreats.Models
{
  public class PierresTreatsContextFactory : IDesignTimeDbContextFactory<PierresTreatsContext>
  {
    PierresTreatsContext IDesignTimeDbContextFactory<PierresTreatsContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<PierresTreatsContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new PierresTreatsContext(builder.Options);
    }
  }
}