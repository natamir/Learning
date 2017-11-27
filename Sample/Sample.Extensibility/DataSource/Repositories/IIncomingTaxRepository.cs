using System.Collections.Generic;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;

namespace Sample.Extensibility.DataSource.Repositories
{
    public interface IIncomingTaxRepository
    {
        IEnumerable<IIncomingTax> GetIncomingTaxes(IIncomingTaxFilter incomingTaxFilter);
    }
}