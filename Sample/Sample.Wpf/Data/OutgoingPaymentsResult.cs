using System.Collections.Generic;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;

namespace Sample.Wpf.Data
{
    internal class OutgoingPaymentsResult
    {
        public OutgoingPaymentsResult(IOutgoingPaymentFilter outgoingPaymentFilter, IReadOnlyCollection<IOutgoingPayment> outgoingPayments)
        {
            OutgoingPaymentFilter = outgoingPaymentFilter;
            OutgoingPayments = outgoingPayments;
        }

        public IOutgoingPaymentFilter OutgoingPaymentFilter { get; }

        public IReadOnlyCollection<IOutgoingPayment> OutgoingPayments { get; }
    }
}