using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sample.Wpf.Extensibility.Initializers
{
    public interface IPieChartInitializer
    {
        void Initialize(Chart pieChart, IDictionary<string, float> data);
    }
}