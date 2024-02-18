using System.Collections.Generic;

namespace WeatherAPI.Models
{
    public class EnumDetails
    {
        public string Name { get; set; }
        public List<EnumValue> Values { get; set; }
    }
}