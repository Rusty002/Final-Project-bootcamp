using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BackEnd.DataLayer
{
    public class SQL_Data_Helper : ISQL_Data_Helper
    {
        string connectionstring = string.Empty;
        List<Country> countries = new List<Country>();
        List<Holiday> holidays = new List<Holiday>();
        public SQL_Data_Helper(IConfiguration config)
        {

            try
            {
                connectionstring = config.GetConnectionString("DefaultConnection");

            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Country> GetCountries()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM COUNTRIES", con))
                    {
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Country country = new Country(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(), Convert.ToInt32(dataReader.GetValue(2)), dataReader.GetValue(3).ToString(), Convert.ToInt32(dataReader.GetValue(4)), Convert.ToInt32(dataReader.GetValue(5)), Convert.ToInt32(dataReader.GetValue(6)));
                                countries.Add(country);
                            }
                        }
                    }

                    con.Close();
                    return countries;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<Holiday> GetHolidays()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM HOLIDAYS", con))
                    {
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Holiday holiday = new Holiday(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(), Convert.ToDateTime(dataReader.GetValue(2)), Convert.ToDateTime(dataReader.GetValue(3)), Convert.ToInt32(dataReader.GetValue(4)));
                                holidays.Add(holiday);
                            }
                        }
                    }

                    con.Close();
                    return holidays;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
