using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int ForecastDay { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }

        public string Advisory
        {
            get
            {
                bool isRainy = Forecast.ToLower().Equals("rain");
                bool isPartlyCloudy = Forecast.ToLower().Equals("partly cloudy");
                bool isThunderstorm = Forecast.ToLower().Equals("thunderstorms");
                bool isSunny = Forecast.ToLower().Equals("sunny");
                bool isSnowy = Forecast.ToLower().Equals("snow");
                bool isCloudy = Forecast.ToLower().Equals("cloudy");

                bool isHot = High > 75;
                bool isCold = Low < 20;
                bool isHotAndCold = High - Low > 20;

                string advisory = String.Empty;

                if (isRainy)
                {
                    advisory = "Pack rain gear and wear waterproof shoes. ";
                }
                else if (isPartlyCloudy)
                {
                    advisory = "Enjoy the weather. ";
                }
                else if (isThunderstorm)
                {
                    advisory = "Seek shelter and avoid hiking on exposed ridges. ";
                }
                else if (isSunny)
                {
                    advisory = "Pack sunblock. ";
                }
                else if (isSnowy)
                {
                    advisory = "Pack snowshoes. ";
                }
                else if (isCloudy)
                {
                    advisory = "Wear a sweater. ";
                }

                if (isHot)
                {
                    advisory += "\r\nBring extra water.";
                }

                if (isHotAndCold)
                {
                    advisory += "\r\nWear breathable layers."; 
                }

                if (isCold)
                {
                    advisory += "\r\nDress warmly.";
                }

                return advisory;
            }
        }
    }
}