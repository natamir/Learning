using System.Collections.Generic;
using System.Linq;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;
using Sample.Wpf.Data;
using Sample.Wpf.Extensibility.Data;
using Sample.Wpf.Extensibility.Providers;

namespace Sample.Wpf.Providers
{
    internal class IncomingTaxChartDataProvider : DateChartDataProviderBase, IIncomingTaxChartDataProvider
    {
        public IChartPairData GetChartData(IReadOnlyCollection<IIncomingTax> incomingTaxes, IIncomingTaxFilter incomingTaxFilter)
        {
            return new ChartPairData
            {
                Ids = incomingTaxes.Select(tax => tax.Id).ToList(),
                AmountPercentData = GetAmountPercentData(incomingTaxes, incomingTaxFilter),
                AmountDateData = GetAmountTimeData(incomingTaxes, incomingTaxFilter)
            };
        }

        private static IDictionary<string, float> GetAmountPercentData(IReadOnlyCollection<IIncomingTax> incomingTaxes, IIncomingTaxFilter incomingTaxFilter)
        {
            Dictionary<string, List<IIncomingTax>> incomingTaxesWithType = incomingTaxFilter.Types.ToDictionary(type => type.ToString(), type => incomingTaxes.Where(tax => tax.Type == type).ToList());
            float totalAmount = incomingTaxesWithType.Sum(taxGroup => taxGroup.Value.Sum(tax => tax.Amount));
            return incomingTaxesWithType.ToDictionary(taxGroup => taxGroup.Key, taxGroup => taxGroup.Value.Sum(tax => tax.Amount) * 100 / totalAmount);
        }
    }
}