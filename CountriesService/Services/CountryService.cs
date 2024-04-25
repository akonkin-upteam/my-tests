using CountriesService.Db;
using CountriesService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace CountriesService.Services
{
    public class CountryService
    {
        //private readonly DbService _dbService;
        private readonly CountryDB _dbcontext;
        public CountryService(CountryDB dbContext) 
        {
            _dbcontext = dbContext;
        }

        public async Task<List<Country>> GetCuntryList()
        {
            return await _dbcontext.Countries.ToListAsync();
        }

        public async Task<List<Province>> GetProvinceList( string countryId ) 
        {
            return await _dbcontext.Provinces
            .Where(p => p.CountryId == countryId)
            .ToListAsync();            
        }

        public async Task<LocationOption> SaveLocationOptions(string userId, string provinceId)
        {
            var locationOption = new LocationOption
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                ProvinceId = provinceId
            };

            _dbcontext.LocationOptions.Add(locationOption);
            await _dbcontext.SaveChangesAsync();
            return locationOption;
        }
    }
}
