using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi_MetricVisualization.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_MetricVisualization.Controllers
{ 
    [Route("api/metric")]
    [ApiController]

    public class MetricController : Controller
    {
        static Dictionary<int, string> mNames = new Dictionary<int, string>();
        static Dictionary<int, List<DateTime>> vNames = new Dictionary<int, List<DateTime>>();

        static int ID = 1; 
        public static Dictionary<int, string> MetricNames
        {
            get
            {
                return mNames;
            }

            set
            {

            }
        }

        public static Dictionary<int, List<DateTime>> MetricValues
        {
            get
            {
                return vNames;
            }

            set
            {

            }
        }


        //GET: metric/get/{metricName}
        [HttpGet("get/{metricName}")]
        public string getMetric(string metricName)
        {
            //получение метрики
            //проверка метрики на существование
            if (MetricNames.ContainsValue(metricName))
            {
                Metric metric = new Metric();
                metric = GetMetric(metricName);
                String output = "ID: " + metric.Id + "\nName: " + metric.Name + "\n";
                output += "Date: ";
                for (int i = 0; i < metric.Values.Count; i++) output += metric.Values[i].ToString() + "\n";
                return output;
            }
            else
            {
                return "Данной метрики не существует.";
            }
        }

        //POST: metric/set/{metricName}
        [HttpPost("set/{metricName}")]
        public void setMetric(string metricName)
        {
            Metric metric = new Metric();
            //если метрики не сущетсвует - создаем
            if (!MetricNames.ContainsValue(metricName))
            {
                metric = CreateMetric(metricName);
                MetricNames.Add(metric.Id, metric.Name);
                MetricValues.Add(metric.Id, metric.Values);
            }
            else
            {
                //если существует - получаем id и записываем дату
                int id = MetricNames.First(c => c.Value == metricName).Key;
                AddMetricValue(id, DateTime.Now);
            }
        }

        //POST: metric/clear/{metricName}
        [HttpPost("clear/{metricName}")]
        public void clearValue(string metricName)
        {
            //если метрика существует - удаляем дату
            if (MetricNames.ContainsValue(metricName))
            {
                int id = MetricNames.First(c => c.Value == metricName).Key;
                MetricValues[id].Clear();
            }
        }

        //POST: metric/delete/{metricName}
        [HttpPost("delete/{metricName}")]
        public void deleteMetric(string metricName)
        {
            //Проверяем метрику - если есть удаляем всё.
            if (MetricNames.ContainsValue(metricName))
            {
                int id = MetricNames.First(c => c.Value == metricName).Key;
                MetricNames.Remove(id);
                MetricValues.Remove(id);
            }
        }


        Metric CreateMetric(string name)
        {
            //создаем новую метрику.
            Metric metric = new Metric();
            metric.Id = ID++;
            metric.Name = name;
            metric.Values.Add(DateTime.Now);
            return metric;

        }

        public void AddMetricValue(int id, DateTime date)
        {
            //добавляем дату.
            MetricValues[id].Add(date);
        }

        Metric GetMetric(string name)
        {
            //получаем ID и через ID возвращаем метрику.
            Metric metric = new Metric();
            int id = MetricNames.First(c => c.Value == name).Key;
            metric.Id = id;
            metric.Name = name;
            metric.Values = MetricValues[id];
            return metric;
        }
    }
}
