using System.Collections.Generic;
using System.Linq;
using Sample.DataSource.Extensibility;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.DataSource.Repositories;
using Sample.Extensibility.Domain.Filters;

namespace Sample.DataSource.Repositories
{
    internal class OutgoingPaymentRepository : PaymentRepositoryBase, IOutgoingPaymentRepository
    {
        private readonly IDataStore dataStore;

        public OutgoingPaymentRepository(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public IEnumerable<IOutgoingPayment> GetOutgoingPayments(IOutgoingPaymentFilter outgoingPaymentFilter)
        {
            return dataStore.OutgoingPayments.Where(
                payment => IsAccountMatch(outgoingPaymentFilter, payment) &&
                           IsTypeMatch(outgoingPaymentFilter, payment) &&
                           IsFromDateMatch(outgoingPaymentFilter, payment) &&
                           IsToDateMatch(outgoingPaymentFilter, payment));
        }

        private static bool IsTypeMatch(IOutgoingPaymentFilter paymentFilter, IOutgoingPayment payment)
        {
            return paymentFilter.Types.Contains(payment.Type);
        }
    }
}