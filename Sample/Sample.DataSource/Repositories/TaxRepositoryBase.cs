using System.Linq;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;

namespace Sample.DataSource.Repositories
{
    internal class TaxRepositoryBase
    {
        protected bool IsPaymentMatch(ITaxFilter taxFilter, ITax tax)
        {
            return taxFilter.PaymentIds.Contains(tax.PaymentId);
        }
    }
}