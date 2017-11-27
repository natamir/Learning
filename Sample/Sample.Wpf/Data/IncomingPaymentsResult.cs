using System.Collections.Generic;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;

namespace Sample.Wpf.Data
{
    internal class IncomingPaymentsResult
    {
        public IncomingPaymentsResult(IIncomingPaymentFilter incomingPaymentFilter, IReadOnlyCollection<IIncomingPayment> incomingPayments)
        {
            IncomingPaymentFilter = incomingPaymentFilter;
            IncomingPayments = incomingPayments;
        }

        public IIncomingPaymentFilter IncomingPaymentFilter { get; }

        public IReadOnlyCollection<IIncomingPayment> IncomingPayments { get; }
    }
}