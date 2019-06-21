using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_MetricVisualization.Models;
using MySql.Data.MySqlClient;

namespace WebApi_MetricVisualization.Interact
{
    public class Methods
    {
        static int ID = 1;
        static Dictionary<int, string> MetricNames = new Dictionary<int, string>();
        static Dictionary<int, List<DateTime>> MetricValues = new Dictionary<int, List<DateTime>>();

        public void AddMetricValue(int id, DateTime date)
        {
            //добавляем дату.
            MetricValues[id].Add(date);
        }
        public string GetMetrics(string metricName)
        {
            //получение метрики
            //проверка метрики на существование
            if (MetricNames.ContainsValue(metricName))
            {
                Metric metric = GetMetric(metricName);
                string output = "ID: " + metric.Id + "\nName: " + metric.Name + "\n";
                output += "Date: ";
                for (int i = 0; i < metric.Values.Count; i++) output += metric.Values[i].ToString() + "\n";
                return output;
            }
            else
            {
                return "Данной метрики не существует.";
            }

        }

        public void SetMetric(string metricName)
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

        public void ClearValue(string metricName)
        {
            //если метрика существует - удаляем дату
            if (MetricNames.ContainsValue(metricName))
            {
                int id = MetricNames.First(c => c.Value == metricName).Key;
                MetricValues[id].Clear();
            }
        }

        public void DeleteMetric(string metricName)
        {
            //Проверяем метрику - если есть удаляем всё.
            if (MetricNames.ContainsValue(metricName))
            {
                int id = MetricNames.First(c => c.Value == metricName).Key;
                MetricNames.Remove(id);
                MetricValues.Remove(id);
            }
        }

        public Metric GetMetric(string name)
        {
            //получаем ID и через ID возвращаем метрику.
            Metric metric = new Metric();
            int id = MetricNames.First(c => c.Value == name).Key;
            metric.Id = id;
            metric.Name = name;
            metric.Values = MetricValues[id];
            return metric;
        }

        public Metric CreateMetric(string name)
        {
            //создаем новую метрику.
            Metric metric = new Metric();
            metric.Id = ID++;
            metric.Name = name;
            metric.Values.Add(DateTime.Now);
            return metric;

        }

        public string GetConnection()
        {
            dbconnection db = new dbconnection();
            string connect = db.ConnectBD();
            try
            {
                MySqlConnection getdb = new MySqlConnection(connect);
                return "Connection successful!";
            }
            catch (Exception e)
            {
                return connect + "Error:" + e.Message;
            }
        }
    }
}
