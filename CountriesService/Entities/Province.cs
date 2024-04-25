namespace CountriesService.Entities
{
    public class Province
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string CountryId { get; set; }
        public Country Country { get; set; }
    }
}
