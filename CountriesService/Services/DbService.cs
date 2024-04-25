using CountriesService.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CountriesService.Services
{
    public class DbService
    {
        private readonly CountryDB _db;
        public DbService(DbContextOptions<CountryDB> dbContextOptions)
        {
            _db = new CountryDB(dbContextOptions);
        }

        public CountryDB DB { get { return _db; } }
    }
}
