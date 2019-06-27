using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using WebApi_MetricVisualization.Models;
using WebApi_MetricVisualization.Repository;

namespace WebApi_MetricVisualization.Controllers
{
    [Route( "api/metric" )]
    [ApiController]

    public class MetricController : Controller
    {
        private readonly SqlRepository SqlRepository;
        public MetricController( IConfiguration config)
        {
            SqlRepository = new SqlRepository( config );
        }

        [HttpGet( "get/{metricName}" )]
        public Metric GetMetric( string metricName )
        {
            return SqlRepository.GetMetrics( metricName );
        }

        [HttpPost( "set/{metricName}" )]
        public void SetData( string metricName )
        {
            SqlRepository.SetMetric( metricName );
        }

        [HttpPost( "clear/{metricName}" )]
        public void ClearData( string metricName )
        {
            SqlRepository.DeleteMetricValue( metricName );

        }

        [HttpPost( "delete/{metricName}" )]
        public void DeleteData( string metricName )
        {
            SqlRepository.DeleteMetric( metricName );
        }

        [HttpGet( "info/{metricName}" )]
        public Dictionary<int, DateTime> InfoMetric( string metricName )
        {
            return SqlRepository.GetMetricByTime( metricName );
        }
    }
}
