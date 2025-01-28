using System.Collections.Generic;

namespace PQAMCClasses.DTOs
{
    public class CountryDTO
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }

        public string CountryCode { get; set; }

        public string Nationality { get; set; }

        public List<CityDTO> Cities { get; set; }
    }
}