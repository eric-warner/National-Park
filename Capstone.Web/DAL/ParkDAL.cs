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

        public ParkDetail GetDetail(string code)
        {
            throw new NotImplementedException();
        }

        private Park GetParkFromReader(SqlDataReader reader)
        {
            return new Park()
            {
                Code = Convert.ToString(reader["parkCode"]),
                Name = Convert.ToString(reader["parkName"]),
                Description = Convert.ToString(reader["parkDescription"])
            };
        }
    }
}