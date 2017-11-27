using System.Collections.Generic;
using Sample.Extensibility.DataSource.Entities;
using Sample.Wpf.Extensibility.Data;

namespace Sample.Wpf.Extensibility.Providers
{
    public interface ITotalTaxChartDataProvider
    {
        IChartPairData GetChartData(IReadOnlyCollection<IIncomingTax> incomingTaxes, IReadOnlyCollection<IOutgoingTax> outgoingTaxes);
    }
}