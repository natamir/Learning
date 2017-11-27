using System.Collections.Generic;
using Sample.Wpf.Extensibility.Data;

namespace Sample.Wpf.Data
{
    public class ChartPairData : IChartPairData
    {
        public IReadOnlyCollection<int> Ids { get; set; }

        public IDictionary<string, float> AmountPercentData { get; set; }

        public IDictionary<string, float> AmountDateData { get; set; }
    }
}