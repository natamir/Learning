using System.Collections.Generic;
using System.Linq;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;
using Sample.Wpf.Data;
using Sample.Wpf.Extensibility.Data;
using Sample.Wpf.Extensibility.Providers;

namespace Sample.Wpf.Providers
{
    internal class OutgoingTaxChartDataProvider : DateChartDataProviderBase, IOutgoingTaxChartDataProvider
    {
        public IChartPairData GetChartData(IReadOnlyCollection<IOutgoingTax> outgoingTaxes, IOutgoingTaxFilter outgoingTaxFilter)
        {
            return new ChartPairData
            {
                Ids = outgoingTaxes.Select(tax => tax.Id).ToList(),
                AmountPercentData = GetAmountPercentData(outgoingTaxes, outgoingTaxFilter),
                AmountDateData = GetAmountTimeData(outgoingTaxes, outgoingTaxFilter)
            };
        }

        private static IDictionary<string, float> GetAmountPercentData(IReadOnlyCollection<IOutgoingTax> outgoingTaxes, IOutgoingTaxFilter outgoingTaxFilter)
        {
            Dictionary<string, List<IOutgoingTax>> outgoingTaxesWithType = outgoingTaxFilter.Types.ToDictionary(type => type.ToString(), type => outgoingTaxes.Where(tax => tax.Type == type).ToList());
            float totalAmount = outgoingTaxesWithType.Sum(taxGroup => taxGroup.Value.Sum(tax => tax.Amount));
            return outgoingTaxesWithType.ToDictionary(taxGroup => taxGroup.Key, taxGroup => taxGroup.Value.Sum(tax => tax.Amount) * 100 / totalAmount);
        }
    }
}