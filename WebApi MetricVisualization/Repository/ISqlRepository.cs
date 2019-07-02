using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WebApi_MetricVisualization.Models;

namespace WebApi_MetricVisualization.Repository
{
    public interface ISqlRepository
    {
        int AddNewMetric( string metricName );
        void AddNewMetricValue( int id );
        void DeleteMetric( string metricName );
        void DeleteMetricName( string metricName );
        void DeleteMetricValue( string metricName );
        MySqlConnection GetConnection();
        int GetId( string metricName );
        Dictionary<int, DateTime> GetMetricByTime( string metricName );
        Metric GetMetrics( string metricName );
        List<DateTime> GetMetricValues( int id );
        void SetMetric( string metricName );
    }
}