using Microsoft.Extensions.Hosting;

namespace CountriesService.Entities
{
    public class Country
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public List<Province>? Provinces { get; set; }        
    }
}
