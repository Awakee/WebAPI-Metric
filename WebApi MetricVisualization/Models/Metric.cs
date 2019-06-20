using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_MetricVisualization.Models
{
    public class Metric
    {

        public int Id;
        public string Name;
        public List<DateTime> Values = new List<DateTime>();

    }
}
