﻿using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using WebApi_MetricVisualization.Models;
using WebApi_MetricVisualization.MetricAgregator;

namespace WebApi_MetricVisualization.Repository
{
    public class SqlRepository
    {

        private readonly IConfiguration configuration;
        public SqlRepository( IConfiguration config )
        {
            configuration = config;
        }

        public MySqlConnection GetConnection()
        {
            string connectionString = configuration.GetConnectionString( "DefaultConnection" );
            MySqlConnection connect = new MySqlConnection( connectionString );
            connect.Open();
            return connect;   
        } 

        public int GetId( string metricName )
        {
            int id = 0;
            string request = $"SELECT Id FROM metric_name WHERE metric_name='{metricName}'";
            MySqlConnection connect = GetConnection();
            MySqlCommand command = new MySqlCommand( request, connect );
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32( reader[0] );
            }
            connect.Close();
            return id;
        }


        public void SetMetric( string metricName )
        {
            int id = GetId( metricName );
            if (id > 0)
            {
                AddNewMetricValue( id );
            }
            else
            {
                id = AddNewMetric( metricName );
                AddNewMetricValue( id );
            }
        }

        public int AddNewMetric( string metricName )
        {
            long id = 0;
            string request = $"INSERT metric_name(metric_name) VALUES ('{metricName}')";
            MySqlConnection connect = GetConnection();
            MySqlCommand command = new MySqlCommand( request, connect );
            command.ExecuteReader();
            id = command.LastInsertedId;
            connect.Close();
            return Convert.ToInt32( id );
        }


        public void AddNewMetricValue( int id )
        {
            string request = $"INSERT metric_value(Id, metric_date) VALUES ({id}, '{DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" )}')";
            MySqlConnection connect = GetConnection();
            MySqlCommand command = new MySqlCommand( request, connect );
            command.ExecuteReader();
            connect.Close();
        }

        public List<DateTime> GetMetricValues( int id )
        {
            List<DateTime> list = new List<DateTime>();
            string request = $"SELECT * FROM metric_value WHERE Id='{id}'";
            MySqlConnection connect = GetConnection();
            MySqlCommand command = new MySqlCommand( request, connect );
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add( Convert.ToDateTime( reader[1] ) );
            }
            connect.Close();
            return list;
        }

        public Metric GetMetrics( string metricName )
        {
            Metric metric = new Metric();
            int id = GetId( metricName );
            if (id > 0)
            {
                metric.Name = metricName;
                metric.Id = id;
                metric.Values = GetMetricValues( id );
                return metric;
            }

            return null;
        }

        public void DeleteMetricValue( string metricName )
        {
            int id = GetId( metricName );
            string request = $"DELETE FROM metric_value WHERE Id={id}";
            if (id > 0)
            {
                MySqlConnection connect = GetConnection();
                MySqlCommand command = new MySqlCommand( request, connect );
                command.ExecuteReader();
                connect.Close();
            }

        }

        public void DeleteMetricName( string metricName )
        {
            int id = GetId( metricName );
            string request = $"DELETE FROM metric_name WHERE Id={id}";
            if (id > 0)
            {
                MySqlConnection connect = GetConnection();
                MySqlCommand command = new MySqlCommand( request, connect );
                command.ExecuteReader();
                connect.Close();
            }
        }

        public void DeleteMetric( string metricName )
        {
            int id = GetId( metricName );
            if (id > 0)
            {
                DeleteMetricValue( metricName );
                DeleteMetricName( metricName );
            }
        }

        public Dictionary<int, DateTime> GetMetricByTime(string metricName)
        {
            int id = GetId( metricName );
            string request = $"SELECT TIME(metric_date), Count(*) FROM metric_value WHERE id = {id} AND (metric_date BETWEEN CURRENT_TIME - INTERVAL 10 MINUTE AND CURRENT_TIME) GROUP BY MINUTE(metric_date)";
            Dictionary<int, DateTime> data = new Dictionary<int, DateTime>();
            MySqlConnection connect = GetConnection();
            MySqlCommand command = new MySqlCommand( request, connect );
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                data.Add( Convert.ToInt32( reader[1] ), Convert.ToDateTime( reader[0].ToString() ) );
            }
            connect.Close();
            return data;
        }

    }

}
