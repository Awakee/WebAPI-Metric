using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApi_MetricVisualization.Interact
{
    public class dbconnection
    {
        public string ConnectBD()
        {
            string host = "localhost";
            int port = 3306;
            string database = "metric_db";
            string username = "root";
            string password = "1234";
            string connect = ($"server={host};port={port};database={database};uid={username};pwd={password};");
            return connect;
        }
        
    }
}
