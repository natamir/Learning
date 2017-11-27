using System.Collections.Generic;
using System.Linq;
using Sample.Extensibility.DataSource.Entities;
using Sample.Wpf.Data;
using Sample.Wpf.Extensibility.Data;
using Sample.Wpf.Extensibility.Providers;

namespace Sample.Wpf.Providers
{
    internal class TotalTaxChartDataProvider : ITotalTaxChartDataProvider
    {
        public IChartPairData GetChartData(IReadOnlyCollection<IIncomingTax> incomingTaxes, IReadOnlyCollection<IOutgoingTax> outgoingTaxes)
        {
            float incomingTaxesAmount = incomingTaxes.Sum(tax => tax.Amount);
            float outcomingTaxesAmount = outgoingTaxes.Sum(tax => tax.Amount);
            return new ChartPairData
            {
                Ids = new int[0],
                AmountPercentData = CreateAmountPercentData(incomingTaxesAmount, outcomingTaxesAmount),
                AmountDateData = CreateAmountDateData(incomingTaxesAmount, outcomingTaxesAmount)
            };
        }

        private static IDictionary<string, float> CreateAmountPercentData(float incomingTaxesAmount, float outcomingTaxesAmount)
        {
            return new Dictionary<string, float>
            {
                { "IncomingTaxes ", incomingTaxesAmount * 100 / (incomingTaxesAmount + outcomingTaxesAmount) },
                { "OutgoingTaxes ", outcomingTaxesAmount * 100 / (incomingTaxesAmount + outcomingTaxesAmount) }
            };
        }

        private static IDictionary<string, float> CreateAmountDateData(float incomingTaxesAmount, float outcomingTaxesAmount)
        {
            return new Dictionary<string, float>
            {
                { "IncomingTaxes ", incomingTaxesAmount },
                { "OutgoingTaxes ", outcomingTaxesAmount }
            };

        }
    }
}