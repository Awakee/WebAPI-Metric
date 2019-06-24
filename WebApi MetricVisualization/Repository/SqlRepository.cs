using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using WebApi_MetricVisualization.Models;

namespace WebApi_MetricVisualization.Repository
{
    public class SqlRepository
    {
        public MySqlConnection GetConnection()
        {
            dbconnection db = new dbconnection();
            string connectdb = db.ConnectBD();
            MySqlConnection connect = new MySqlConnection( connectdb );
            return connect;
        } 

        public int GetID( string metricName )
        {
            int id = 0;
            string request = $"SELECT Id FROM metric_name WHERE MetricName='{metricName}'";
            MySqlConnection connect = GetConnection();
            connect.Open();
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
            int id = GetID( metricName );
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
            string request = $"INSERT metric_name(MetricName) VALUES ('{metricName}')";
            MySqlConnection connect = GetConnection();
            connect.Open();
            MySqlCommand command = new MySqlCommand( request, connect );
            command.ExecuteReader();
            id = command.LastInsertedId;
            connect.Close();
            return Convert.ToInt32( id );
        }


        public void AddNewMetricValue( int id )
        {
            string request = $"INSERT metric_value(Id, MetricValue) VALUES ({id}, '{DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" )}')";
            MySqlConnection connect = GetConnection();
            connect.Open();
            MySqlCommand command = new MySqlCommand( request, connect );
            command.ExecuteReader();
            connect.Close();
        }

        public List<DateTime> GetMetricValues( int id )
        {
            List<DateTime> list = new List<DateTime>();
            string request = $"SELECT * FROM metric_value WHERE Id='{id}'";
            MySqlConnection connect = GetConnection();
            connect.Open();
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
            int id = GetID( metricName );
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
            int id = GetID( metricName );
            string request = $"DELETE FROM metric_value WHERE Id={id}";
            if (id > 0)
            {
                MySqlConnection connect = GetConnection();
                connect.Open();
                MySqlCommand command = new MySqlCommand( request, connect );
                command.ExecuteReader();
                connect.Close();
            }

        }

        public void DeleteMetricName( string metricName )
        {
            int id = GetID( metricName );
            string request = $"DELETE FROM metric_name WHERE Id={id}";
            if (id > 0)
            {
                MySqlConnection connect = GetConnection();
                connect.Open();
                MySqlCommand command = new MySqlCommand( request, connect );
                command.ExecuteReader();
                connect.Close();
            }
        }

        public void DeleteMetric( string metricName )
        {
            int id = GetID( metricName );
            if (id > 0)
            {
                DeleteMetricValue( metricName );
                DeleteMetricName( metricName );
            }
        }

    }

}
