using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi_MetricVisualization.Interact;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_MetricVisualization.Controllers
{
    [Route("api/metric")]
    [ApiController]

    public class MetricController : Controller
    {
        Methods getMethod = new Methods();

        //GET: metric/get/{metricName}
        [HttpGet("get/{metricName}")]

        public string GetMetric(string metricName)
        {
            return getMethod.GetMetrics(metricName);
        }

        //POST: metric/set/{metricName}
        [HttpPost("set/{metricName}")]
        public string SetData(string metricName)
        {
           return getMethod.SetMetric(metricName);
        }


        //POST: metric/clear/{metricName}
        [HttpPost("clear/{metricName}")]
        public string ClearData(string metricName)
        {
            return getMethod.ClearValue(metricName);

        }

        //POST: metric/delete/{metricName}
        [HttpPost("delete/{metricName}")]
        public string DeleteData(string metricName)
        {
            return getMethod.DeleteMetric(metricName);
        }


    }
}
