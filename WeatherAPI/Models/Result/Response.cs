using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    public class Response<T>
    {
        public ResponseStatus Status { get; set; }
        public List<EnumDetails> Enums { get; set; }
        public T Details { get; set; }
    }
}
