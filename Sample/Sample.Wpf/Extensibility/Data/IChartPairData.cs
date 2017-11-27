using System.Collections.Generic;

namespace Sample.Wpf.Extensibility.Data
{
    public interface IChartPairData
    {
        IReadOnlyCollection<int> Ids { get; }

        IDictionary<string, float> AmountPercentData { get; }

        IDictionary<string, float> AmountDateData { get; }
    }
}