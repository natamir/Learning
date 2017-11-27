using System.Collections.Generic;
using System.Linq;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;
using Sample.Wpf.Data;
using Sample.Wpf.Extensibility.Data;
using Sample.Wpf.Extensibility.Providers;

namespace Sample.Wpf.Providers
{
    internal class IncomingPaymentChartDataProvider : DateChartDataProviderBase, IIncomingPaymentChartDataProvider
    {
        public IChartPairData GetChartData(IReadOnlyCollection<IIncomingPayment> incomingPayments, IIncomingPaymentFilter incomingPaymentFilter)
        {
            return new ChartPairData
            {
                Ids = incomingPayments.Select(payment => payment.Id).ToList(),
                AmountPercentData = GetAmountPercentData(incomingPayments, incomingPaymentFilter),
                AmountDateData = GetAmountTimeData(incomingPayments, incomingPaymentFilter)
            };
        }

        private static IDictionary<string, float> GetAmountPercentData(IReadOnlyCollection<IIncomingPayment> incomingPayments, IIncomingPaymentFilter incomingPaymentFilter)
        {
            Dictionary<string, List<IIncomingPayment>> incomingPaymentWithType = incomingPaymentFilter.Types.ToDictionary(type => type.ToString(), type => incomingPayments.Where(payment => payment.Type == type).ToList());
            float totalAmount = incomingPaymentWithType.Sum(paymentGroup => paymentGroup.Value.Sum(payment => payment.Amount));
            return incomingPaymentWithType.ToDictionary(paymentGroup => paymentGroup.Key, paymentGroup => paymentGroup.Value.Sum(payment => payment.Amount) * 100 / totalAmount);
        }
    }
}