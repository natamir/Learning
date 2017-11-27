using System.Collections.Generic;
using System.Linq;
using Sample.DataSource.Extensibility;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.DataSource.Repositories;
using Sample.Extensibility.Domain.Filters;

namespace Sample.DataSource.Repositories
{
    internal class IncomingTaxRepository : TaxRepositoryBase, IIncomingTaxRepository
    {
        private readonly IDataStore dataStore;

        public IncomingTaxRepository(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public IEnumerable<IIncomingTax> GetIncomingTaxes(IIncomingTaxFilter incomingTaxFilter)
        {
            return dataStore.IncomingTaxes.Where(
                tax => IsPaymentMatch(incomingTaxFilter, tax) &&
                        IsTypeMatch(incomingTaxFilter, tax));
        }

        private static bool IsTypeMatch(IIncomingTaxFilter incomingTaxFilter, IIncomingTax tax)
        {
            return incomingTaxFilter.Types.Contains(tax.Type);
        }
    }
}