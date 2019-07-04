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
        private readonly ISqlRepository _sqlRepository;
        private readonly IAgregator _agregator;

        public MetricController( IAgregator agregator, ISqlRepository sqlRepository )
        {
            _sqlRepository = sqlRepository;
            _agregator = agregator;
        }
        public IActionResult Index()
        {
            ViewBag.result = _sqlRepository.GetAllMetric();
            return View();
        }

        [HttpGet( "get/{metricName}" )]
        public Metric GetMetric( string metricName )
        {
            return _sqlRepository.GetMetrics( metricName );
        }

        [HttpPost( "set/{metricName}" )]
        public void SetData( string metricName )
        {
            _sqlRepository.SetMetric( metricName );
        }

        [HttpPost( "clear/{metricName}" )]
        public void ClearData( string metricName )
        {
            _sqlRepository.DeleteMetricValue( metricName );
        }

        [HttpPost( "delete/{metricName}" )]
        public void DeleteData( string metricName )
        {
            _sqlRepository.DeleteMetric( metricName );
        }

        [HttpGet( "info/{metricName}" )]
        public Dictionary<int, int> InfoMetric( string metricName )
        {
            return _sqlRepository.GetMetricByTime( metricName );
        }

        [HttpGet( "interval/{metricName}" )]
        public Dictionary<int, (int, int)> Interval( string metricName )
        {
            return _agregator.GetCounts( metricName );

        }

        [Route( "graph/{metricName}" )]
        public IActionResult Graph( string metricName )
        {
            ViewBag.result = metricName;
            return View();
        }


    }
}
