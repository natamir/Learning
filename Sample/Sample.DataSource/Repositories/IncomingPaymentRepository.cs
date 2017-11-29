using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using Sample.DataSource.Extensibility.Data;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.DataSource.Repositories;
using Sample.Extensibility.Domain.Filters;

namespace Sample.DataSource.Repositories
{
    internal class IncomingPaymentRepository : PaymentRepositoryBase, IIncomingPaymentRepository
    {
        private readonly IDataStore dataStore;

        public IncomingPaymentRepository(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public IEnumerable<IIncomingPayment> GetIncomingPayments(IIncomingPaymentFilter incomingPaymentFilter)
        {
            Expression<Func<IIncomingPayment, bool>> predicate =
                payment => IsAccountMatch(incomingPaymentFilter, payment) &&
                           IsTypeMatch(incomingPaymentFilter, payment) &&
                           IsFromDateMatch(incomingPaymentFilter, payment) &&
                           IsToDateMatch(incomingPaymentFilter, payment);
            return dataStore.IncomingPayments.AsQueryable().Where(predicate.Compile());
        }

        private static bool IsTypeMatch(IIncomingPaymentFilter paymentFilter, IIncomingPayment payment)
        {
            return paymentFilter.Types.Contains(payment.Type);
        }
    }
}