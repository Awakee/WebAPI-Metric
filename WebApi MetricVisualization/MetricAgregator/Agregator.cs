using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using WebApi_MetricVisualization.Repository;

namespace WebApi_MetricVisualization.MetricAgregator
{
    public class Agregator
    {
        private readonly SqlRepository SqlRepository;
        public Agregator( IConfiguration config )
        {
            SqlRepository = new SqlRepository( config );
        }


        public Dictionary<int, int> GetCounts(string metricName)
        {
            Dictionary<int, int> newdata = GetTimeInterval();
            Dictionary<int, int> data = SqlRepository.GetMetricByTime( metricName );
            Dictionary<int, int> result = new Dictionary<int, int>();
            foreach (var keyValue in newdata)
            {
                {
                    if (newdata.ContainsKey(keyValue.Key) == data.ContainsKey(keyValue.Key))
                    {
                        result.Add( keyValue.Key, data[keyValue.Key] );
                    } else
                    {
                        result.Add( keyValue.Key, 0 );
                    }
                }
                
            }
                return result;
        }

        public Dictionary<int, int> GetTimeInterval()
        {
            DateTime time = DateTime.Now;
            time = time.AddMinutes( -10 );
            int campare = DateTime.Compare( time, DateTime.Now );
            Dictionary<int, int> datax = new Dictionary<int, int>();
            while (campare < 0)
            {
                datax.Add(time.Minute,  0);
                time = time.AddMinutes( 1 );
                campare = DateTime.Compare( time, DateTime.Now );
            }
            return datax;
        }

    }
}
