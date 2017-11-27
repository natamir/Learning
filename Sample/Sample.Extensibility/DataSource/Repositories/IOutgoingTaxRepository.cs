using System.Collections.Generic;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;

namespace Sample.Extensibility.DataSource.Repositories
{
    public interface IOutgoingTaxRepository
    {
        IEnumerable<IOutgoingTax> GetOutgoingTaxes(IOutgoingTaxFilter outgoingPaymentFilter);
    }
}