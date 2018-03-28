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
            string SQL_GetAllParks = @"SELECT * FROM park ORDER BY parkName";

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

        public bool AddSurvey(Survey survey)
        {
            bool isAdded = false;
            string SQL_AddReview = @"INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) 
                                     VALUES (@code, @email, @state, @activity);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_AddReview, conn);
                cmd.Parameters.AddWithValue("@code", survey.ParkCode);
                cmd.Parameters.AddWithValue("@email", survey.Email);
                cmd.Parameters.AddWithValue("@state", survey.State);
                cmd.Parameters.AddWithValue("@activity", survey.ActivityLevel);

                int result = (int)cmd.ExecuteNonQuery();

                if(result > 0)
                {
                    isAdded = true;
                }
            }

            return isAdded;
        }

        public IList<SurveyResult> GetSurveyResults()
        {
            List<SurveyResult> results = new List<SurveyResult>();
            string SQL_GetSurveyResults = @"SELECT p.*, s.surveyCount
                                            FROM (
	                                              SELECT parkCode, COUNT(*) AS surveyCount 
	                                              FROM survey_result 
	                                              GROUP BY parkCode
	                                              ) s
                                            JOIN park p ON s.parkCode = p.parkCode
                                            ORDER BY s.surveyCount DESC, p.parkName ASC;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_GetSurveyResults, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    results.Add(GetParkAndSurveyResult(reader));
                }
            }

            return results;
        }

        #region From Reader Functions
        private Park GetParkFromReader(SqlDataReader reader)
        {
            return new Park()
            {
                Code = Convert.ToString(reader["parkCode"]),
                Name = Convert.ToString(reader["parkName"]),
                State = Convert.ToString(reader["state"]),
                Acreage = Convert.ToInt32(reader["acreage"]),
                ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]),
                MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]),
                NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                Climate = Convert.ToString(reader["climate"]),
                YearFounded = Convert.ToInt32(reader["yearFounded"]),
                AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                Description = Convert.ToString(reader["parkDescription"]),
                EntryFee = Convert.ToInt32(reader["entryFee"]),
                NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"])
            };
        }

        private SurveyResult GetParkAndSurveyResult(SqlDataReader reader)
        {
            return new SurveyResult()
            {
                Park = GetParkFromReader(reader),
                Count = Convert.ToInt32(reader["surveyCount"])
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
        #endregion
    }
}