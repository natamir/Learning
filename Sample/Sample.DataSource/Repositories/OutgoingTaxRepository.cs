using System.Collections.Generic;
using System.Linq;
using Sample.DataSource.Extensibility;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.DataSource.Repositories;
using Sample.Extensibility.Domain.Filters;

namespace Sample.DataSource.Repositories
{
    internal class OutgoingTaxRepository : TaxRepositoryBase, IOutgoingTaxRepository
    {
        private readonly IDataStore dataStore;

        public OutgoingTaxRepository(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public IEnumerable<IOutgoingTax> GetOutgoingTaxes(IOutgoingTaxFilter outgoingPaymentFilter)
        {
            return dataStore.OutgoingTaxes.Where(
                tax => IsPaymentMatch(outgoingPaymentFilter, tax) &&
                       IsTypeMatch(outgoingPaymentFilter, tax));
        }

        private static bool IsTypeMatch(IOutgoingTaxFilter taxFilter, IOutgoingTax tax)
        {
            return taxFilter.Types.Contains(tax.Type);
        }
    }
}