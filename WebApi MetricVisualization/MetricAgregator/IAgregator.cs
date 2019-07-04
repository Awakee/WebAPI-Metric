using System.Collections.Generic;

namespace WebApi_MetricVisualization.MetricAgregator
{
    public interface IAgregator
    {
        Dictionary<int, (int, int)> GetCounts( string metricName );
        Dictionary<int, int> GetTimeInterval();
    }
}