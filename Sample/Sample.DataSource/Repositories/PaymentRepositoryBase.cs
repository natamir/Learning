using System.Linq;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;

namespace Sample.DataSource.Repositories
{
    internal class PaymentRepositoryBase
    {
        protected bool IsAccountMatch(IPaymentFilter paymentFilter, IPayment payment)
        {
            return paymentFilter.AccountIds.Contains(payment.AccountId);
        }

        protected bool IsFromDateMatch(IPaymentFilter paymentFilter, IPayment payment)
        {
            return paymentFilter.FromDate <= payment.Date;
        }

        protected bool IsToDateMatch(IPaymentFilter paymentFilter, IPayment payment)
        {
            return paymentFilter.ToDate >= payment.Date;
        }
    }
}