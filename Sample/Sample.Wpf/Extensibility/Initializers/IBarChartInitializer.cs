using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sample.Wpf.Extensibility.Initializers
{
    public interface IBarChartInitializer
    {
        void Initialize(Chart barChart, IDictionary<string, float> data);
    }
}