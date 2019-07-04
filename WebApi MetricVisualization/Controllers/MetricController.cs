using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using WebApi_MetricVisualization.Models;
using WebApi_MetricVisualization.Repository;
using WebApi_MetricVisualization.MetricAgregator;

namespace WebApi_MetricVisualization.Controllers
{
    [ApiController]
    [Route( "api/metric" )]
    public class MetricController : Controller
    {
        
        public IActionResult Index()
        {
            ViewBag.result = SqlRepository.GetAllMetric();
            return View();
        }

        private readonly SqlRepository SqlRepository;
        private readonly Agregator Agregator;

        public MetricController( IConfiguration config, ISqlRepository sqlRepository)
        {
            SqlRepository = new SqlRepository( config );
            Agregator = new Agregator( config );

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
        public Dictionary<int, int> InfoMetric( string metricName )
        {
            return SqlRepository.GetMetricByTime( metricName );
        }

        [HttpGet( "interval/{metricName}" )]
        public Dictionary<int, (int, int)> Interval( string metricName )
        {
            return Agregator.GetCounts( metricName );
            
        }

        [Route( "graph/{metricName}" )]
        public IActionResult Graph(string metricName)
        {
            ViewBag.result = metricName;
            return View();
        }


    }
}
