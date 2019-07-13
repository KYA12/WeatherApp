﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Configuration;

namespace WeatherAppWPF 
{
    public class DataAccess
    {
        public static string getConnectionString()
        {
            string sql = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\WeatherAPIDB.mdf;Integrated Security=True;MultipleActiveResultSets=True;";
            return sql;
        }

        public static bool ExecuteNonQuery(string strSQL,
           CommandType cmdType, params SqlParameter[] paramList)
        {
            bool result;
            SqlConnection cnn = new SqlConnection(getConnectionString());
            SqlCommand cmd = new SqlCommand(strSQL, cnn);
            cmd.CommandType = cmdType;
            cmd.Parameters.AddRange(paramList);
            try
            {
                cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public static DataTable ExecuteQueryWithDataTable(string strSQL, CommandType cmdType,
          params SqlParameter[] param)
        {
            SqlConnection cnn = new SqlConnection(getConnectionString());
            SqlCommand cmd = new SqlCommand(strSQL, cnn);
            cmd.CommandType = cmdType;
            if (param != null)
                cmd.Parameters.AddRange(param);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
  
        public DataTable GetCityData()
        {
            string sql = "SELECT CityData.Id, CityData.CityName, CityData.RainTime from CityData";
            DataTable dt = DataAccess.ExecuteQueryWithDataTable(sql, CommandType.Text);
            return dt;
        }
       
        public bool EditCityData(CityData data)
        {
            bool check = false;
            try
            { 
                string sql = "UPDATE CityData SET CityName = @name, RainTime = @raintime WHERE Id =@ID ";
                DateTime startTime = DateTime.Parse(data.RainTime[0],
                           null, DateTimeStyles.AssumeUniversal);
                SqlParameter Id = new SqlParameter("@ID", data.Id);
                SqlParameter Name = new SqlParameter("@name", data.CityName);
                SqlParameter RainTime = new SqlParameter("@raintime", startTime.DayOfWeek.ToString());
                check = DataAccess.ExecuteNonQuery(sql, CommandType.Text, Id, Name, RainTime);
            }
            catch
            {
                MessageBox.Show("Неправильный формат данных");
            }
            return check;
        }
    }
}
