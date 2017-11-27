using System.Collections.Generic;
using System.Linq;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;
using Sample.Wpf.Data;
using Sample.Wpf.Extensibility.Data;
using Sample.Wpf.Extensibility.Providers;

namespace Sample.Wpf.Providers
{
    internal class OutgoingPaymentChartDataProvider : DateChartDataProviderBase, IOutgoingPaymentChartDataProvider
    {
        public IChartPairData GetChartData(IReadOnlyCollection<IOutgoingPayment> outgoingPayments, IOutgoingPaymentFilter outgoingPaymentFilter)
        {
            return new ChartPairData
            {
                Ids = outgoingPayments.Select(payment => payment.Id).ToList(),
                AmountPercentData = GetAmountPercentData(outgoingPayments, outgoingPaymentFilter),
                AmountDateData = GetAmountTimeData(outgoingPayments, outgoingPaymentFilter)
            };
        }

        private static IDictionary<string, float> GetAmountPercentData(IReadOnlyCollection<IOutgoingPayment> outgoingPayments, IOutgoingPaymentFilter outgoingPaymentFilter)
        {
            Dictionary<string, List<IOutgoingPayment>> incomingPaymentWithType = outgoingPaymentFilter.Types.ToDictionary(type => type.ToString(), type => outgoingPayments.Where(payment => payment.Type == type).ToList());
            float totalAmount = incomingPaymentWithType.Sum(paymentGroup => paymentGroup.Value.Sum(payment => payment.Amount));
            return incomingPaymentWithType.ToDictionary(paymentGroup => paymentGroup.Key, paymentGroup => paymentGroup.Value.Sum(payment => payment.Amount) * 100 / totalAmount);
        }
    }
}