using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class ParkDAL : IParkDAL
    {
        private string _connectionString;
        public ParkDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<Park> GetAllParks()
        {
            List<Park> parks = new List<Park>();
            string SQL_GetAllParks = @"SELECT parkCode, parkName, parkDescription FROM park ORDER BY parkName";

            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_GetAllParks, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    parks.Add(GetParkFromReader(reader));
                }
            }

            return parks;
        }

        public Park GetPark(string code)
        {
            Park park = null;
            string SQL_GetPark = @"SELECT * FROM Park WHERE parkCode = @code";

            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_GetPark, conn);
                cmd.Parameters.AddWithValue("@code", code);

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    park = GetParkFromReader(reader);
                }
            }

            return park;
        }

        public IList<Weather> GetParkWeather(string code)
        {
            List<Weather> weather = new List<Weather>();
            string SQL_GetParkWeather = @"SELECT * FROM weather WHERE parkCode = @code";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_GetParkWeather, conn);
                cmd.Parameters.AddWithValue("@code", code);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    weather.Add(GetWeatherFromReader(reader));
                }
            }

            return weather;
        }

        private Park GetParkFromReader(SqlDataReader reader)
        {
            return new Park()
            {
                Code = Convert.ToString(reader["parkCode"]),
                Name = Convert.ToString(reader["parkName"]),
                Description = Convert.ToString(reader["parkDescription"]),
                State = Convert.ToString(reader["state"]),
                Acreage = Convert.ToInt32(reader["acreage"]),
                ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]),
                MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]),
                Climate = Convert.ToString(reader["climate"]),
                YearFounded = Convert.ToInt32(reader["yearFounded"]),
                AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                EntryFee = Convert.ToInt32(reader["entryFee"]),
                NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"])
            };
        }

        private Weather GetWeatherFromReader(SqlDataReader reader)
        {
            return new Weather()
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                ForecastDay = Convert.ToInt32(reader["fiveDayForecastValue"]),
                Low = Convert.ToInt32(reader["low"]),
                High = Convert.ToInt32(reader["high"]),
                Forecast = Convert.ToString(reader["forecast"])
            };
        }
    }
}