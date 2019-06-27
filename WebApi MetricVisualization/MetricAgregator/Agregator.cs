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


        public int[,] GetCounts(string metricName)
        {

            int[,] array = new int[25, 2];
            int[,] arrayTime = GetTimeInterval();
            Dictionary<int, DateTime> newdata = new Dictionary<int, DateTime>();
            newdata = SqlRepository.GetMetricByTime( metricName );
            foreach (KeyValuePair<int, DateTime> keyValue in newdata)
            {
                for (int i = 0; i < arrayTime.GetLength(0); i++)
                {
                    if (keyValue.Value.Minute == arrayTime[i, 0])
                    {
                        arrayTime[i, 1] = keyValue.Key;
                    }
                }
                
            }
                return arrayTime;
        }

        public int[,] GetTimeInterval()
        {
            int[,] array = new int[11, 2];
            DateTime time = DateTime.Now;
            time = time.AddMinutes( -10 );
            int campare = DateTime.Compare( time, DateTime.Now );
            int i = 0;
            Dictionary<int, int> data = new Dictionary<int, int>();
            while (campare < 0)
            {
                array[i,0] = time.Minute;
                i++;
                time = time.AddMinutes( 1 );
                campare = DateTime.Compare( time, DateTime.Now );
            }

            return array;
        }

    }
}
