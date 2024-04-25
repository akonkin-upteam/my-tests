using CountriesService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CountriesService.Db
{
    public class CountryDB : DbContext
    {
        public CountryDB(DbContextOptions<CountryDB> options) : base(options) { }
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Province> Provinces{ get; set; } = null!;
        public DbSet<LocationOption> LocationOptions { get; set; } = null!;
    }
    
}
