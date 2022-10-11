using System.Text.Json.Serialization;

namespace FlightPlanner.Models
{
    public class Airport
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        [JsonPropertyName("airport")]
        public string AirportName { get; set; }

        public bool Equals(Airport airport)
        {
            if (airport == null)
            {
                return false;
            }

            var country = Country == airport.Country;
            var city = City == airport.City;
            var airportName = AirportName == airport.AirportName;
            return country && city && airportName;
        }
    }
}
