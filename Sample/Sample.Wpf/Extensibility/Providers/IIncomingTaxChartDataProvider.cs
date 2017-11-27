using System.Collections.Generic;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;
using Sample.Wpf.Extensibility.Data;

namespace Sample.Wpf.Extensibility.Providers
{
    public interface IIncomingTaxChartDataProvider
    {
        IChartPairData GetChartData(IReadOnlyCollection<IIncomingTax> incomingTaxes, IIncomingTaxFilter incomingTaxFilter);
    }
}