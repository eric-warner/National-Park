using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ParkAndWeather
    {
        public Park Park { get; set; }
        public IList<Weather> Weather { get; set; } = new List<Weather>();
    }
}