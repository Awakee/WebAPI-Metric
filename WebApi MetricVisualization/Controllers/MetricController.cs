using Microsoft.AspNetCore.Mvc;
using WebApi_MetricVisualization.Models;
using WebApi_MetricVisualization.Repository;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_MetricVisualization.Controllers
{
    [Route( "api/metric" )]
    [ApiController]

    public class MetricController : Controller
    {
        SqlRepository getMethod = new SqlRepository();

        //GET: metric/get/{metricName}
        [HttpGet( "get/{metricName}" )]

        public Metric GetMetric( string metricName )
        {
            return getMethod.GetMetrics( metricName );
        }

        //POST: metric/set/{metricName}
        [HttpPost( "set/{metricName}" )]
        public void SetData( string metricName )
        {
            getMethod.SetMetric( metricName );
        }


        //POST: metric/clear/{metricName}
        [HttpPost( "clear/{metricName}" )]
        public void ClearData( string metricName )
        {
            getMethod.DeleteMetricValue( metricName );

        }

        //POST: metric/delete/{metricName}
        [HttpPost( "delete/{metricName}" )]
        public void DeleteData( string metricName )
        {
            getMethod.DeleteMetric( metricName );
        }

        [HttpGet( "test/{metricName}" )]
        public Metric test( string metricName )
        {
            return getMethod.GetMetricByTime( metricName );
        }
    }
}
