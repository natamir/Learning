using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using Sample.DataSource.Extensibility;
using Sample.DataSource.Extensibility.Data;
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
            Expression<Func<IIncomingTax, bool>> predicate = tax => IsPaymentMatch(incomingTaxFilter, tax) &&
                                                                    IsTypeMatch(incomingTaxFilter, tax);
            return dataStore.IncomingTaxes.AsQueryable().Where(predicate.Compile());
        }

        private static bool IsTypeMatch(IIncomingTaxFilter incomingTaxFilter, IIncomingTax tax)
        {
            return incomingTaxFilter.Types.Contains(tax.Type);
        }
    }
}