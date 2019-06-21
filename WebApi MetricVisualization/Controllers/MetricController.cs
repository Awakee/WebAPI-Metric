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
        Methods getData = new Methods();

        //GET: metric/get/{metricName}
        [HttpGet("get/{metricName}")]

        public string GetData(string metricName)
        {
            return getData.GetMetrics(metricName);
        }

        //POST: metric/set/{metricName}
        [HttpPost("set/{metricName}")]
        public void SetData(string metricName)
        {
            getData.SetMetric(metricName);
        }
        

        //POST: metric/clear/{metricName}
        [HttpPost("clear/{metricName}")]
        public void ClearData(string metricName)
        {
            getData.ClearValue(metricName);

        }

        //POST: metric/delete/{metricName}
        [HttpPost("delete/{metricName}")]
        public void DeleteData(string metricName)
        {
            getData.DeleteMetric(metricName);
        }


        //GET: metric/check
        [HttpGet("check")]
        public string GetConnection()
        {
            return getData.GetConnection();
        }

       
        
    }
}
