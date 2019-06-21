using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi_MetricVisualization.Interact;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_MetricVisualization.Controllers
{
    [Route("api/metric")]
    [ApiController]

    public class MetricController : Controller
    {
        
        //GET: metric/get/{metricName}
        [HttpGet("get/{metricName}")]

        public string GetData(string metricName)
        {
            Methods getData = new Methods();
            return getData.GetMetrics(metricName);
        }

        //POST: metric/set/{metricName}
        [HttpPost("set/{metricName}")]
        public void SetData(string metricName)
        {
            Methods getData = new Methods();
            getData.SetMetric(metricName);

        }
        

        //POST: metric/clear/{metricName}
        [HttpPost("clear/{metricName}")]
        public void ClearData(string metricName)
        {
            Methods getData = new Methods();
            getData.ClearValue(metricName);

        }

        //POST: metric/delete/{metricName}
        [HttpPost("delete/{metricName}")]
        public void DeleteData(string metricName)
        {
            Methods getData = new Methods();
            getData.DeleteMetric(metricName);
        }
       
    }
}
