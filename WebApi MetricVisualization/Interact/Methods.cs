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
        //Получаем метрику по значению.
        public string GetMetrics(string metricName)
        {
            try
            {
                //проверяем на существование
                if (CheckExist(metricName))
                {
                    string result = null;
                    string request = $"SELECT * FROM metric_name WHERE MetricName='{metricName}'";
                    result += GetConnection(request, "getData");
                    int id = GetID(metricName);
                    string requestDate = $"SELECT * FROM metric_value WHERE Id='{id}'";
                    result += GetConnection(requestDate, "getDate");
                    return result;
                }
                return null;
            }
            catch(Exception e)
            {
                return "Error:" + e.Message;
            }
        }

        //Проверяем существования записи.
        public bool CheckExist(string metricName)
        {
            string request = $"SELECT Id FROM metric_name WHERE MetricName='{metricName}'";
            bool answer = Convert.ToBoolean(GetConnection(request, "general"));
            if (answer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //получаем ID в БД по значению.
        public int GetID(string metricName)
        {
            if (CheckExist(metricName)){
                string request = $"SELECT Id FROM metric_name WHERE MetricName='{metricName}'";
                string answer = GetConnection(request, "existId");
                return Convert.ToInt32(answer);
            }
            else
            {
                return 0;
            }
        }
        
        //Добавляем дату, + добавляем запись если она отсутствует.
        public string SetMetric(string metricName)
        {
            //Если метрики не существует, добавляем запись.
            try
            {
                if(!CheckExist(metricName))
                {
                    //Добавляем новую метрику ID+Name
                    string request = $"INSERT metric_name(MetricName) VALUES ('{metricName}')";
                    int id = int.Parse(GetConnection(request, "getId"));
                    //Добавляем новую метрику в таблицу с датами
                    string requestData = $"INSERT metric_value(Id, MetricValue) VALUES ({id}, '{DateTime.Now}')";
                    GetConnection(requestData, null);
                    return $"Record successfully added! Name: {metricName} + added date: {DateTime.Now}";
                }
                else
                {
                    //Получаем ID и записываем в таблицу
                    int id = GetID(metricName);
                    string requestData = $"INSERT metric_value(Id, MetricValue) VALUES ({id}, '{DateTime.Now}')";
                    GetConnection(requestData, null);
                    return "Added new date";
                }

            }
            catch(Exception e)
            {
                return "Error:" + e.Message;
            }
        }
        


        public string GetConnection(string request, string operationName)
        {
            try
            {
                //подключаемся к БД + выполняем запрос
                dbconnection db = new dbconnection();
                string connectdb = db.ConnectBD();
                MySqlConnection connect = new MySqlConnection(connectdb);
                connect.Open();
                MySqlCommand command = new MySqlCommand(request, connect);
                MySqlDataReader reader = command.ExecuteReader();

                switch (operationName)
                {
                    case "getId":                   //получаем ID последней записи
                        long id = command.LastInsertedId;
                        connect.Close();
                        return id.ToString();
                    case "general":                 //выполнение запроса + получение ответа
                        return reader.Read().ToString();
                    case "existId":                 //получаем ID по значению(запросу)
                        reader.Read();
                        return reader[0].ToString();
                    case "getData":                 //получение всей информации по запросу
                        string result = null;
                        while (reader.Read())
                        {
                            result += "ID:" + reader[0].ToString() + "\t Name: " + reader[1].ToString() + "\n";
                        }
                        return result;
                    case "getDate":                 //получаем дату по запросу
                        string resultDate = null;
                        while (reader.Read())
                        {
                            resultDate += "\t Date: " + reader[1].ToString() + "\n";
                        }
                        return resultDate;
                }
                return "Error";
            }
            catch (Exception e)
            {
                return "Error:" + e.Message;
            }

        }

        //удаляем значения дат по ID
        public string ClearValue(string metricName)
        {
            if (CheckExist(metricName))
            {
                try
                {
                    int id = GetID(metricName);
                    string request = $"DELETE FROM metric_value WHERE Id={id}";
                    GetConnection(request, null);
                    return "Dates with " + id + " deleted.";
                }
                catch(Exception e)
                {
                    return "Error" + e.Message;
                }
                
            }
            return null;
        }

        //удаляем метрику по ID
        public string DeleteMetric(string metricName)
        {
            if (CheckExist(metricName))
            {
                try
                {
                    int id = GetID(metricName);
                    string request = $"DELETE FROM metric_name WHERE Id={id}";
                    GetConnection(request, null);
                    string requestDate = $"DELETE FROM metric_value WHERE Id={id}";
                    GetConnection(requestDate, null);
                    return $"Metric {metricName} with id: {id} and all dates deleted";

                }
                catch(Exception e)
                {
                    return e.Message;
                }
            }
            return null;
        }  
    }
}
