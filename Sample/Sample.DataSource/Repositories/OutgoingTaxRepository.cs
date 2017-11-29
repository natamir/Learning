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
    internal class OutgoingTaxRepository : TaxRepositoryBase, IOutgoingTaxRepository
    {
        private readonly IDataStore dataStore;

        public OutgoingTaxRepository(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public IEnumerable<IOutgoingTax> GetOutgoingTaxes(IOutgoingTaxFilter outgoingPaymentFilter)
        {
            Expression<Func<IOutgoingTax, bool>> predicate = tax => IsPaymentMatch(outgoingPaymentFilter, tax) &&
                                                                    IsTypeMatch(outgoingPaymentFilter, tax);
            return dataStore.OutgoingTaxes.AsQueryable().Where(predicate.Compile());
        }

        private static bool IsTypeMatch(IOutgoingTaxFilter taxFilter, IOutgoingTax tax)
        {
            return taxFilter.Types.Contains(tax.Type);
        }
    }
}