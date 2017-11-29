using System.Collections.Generic;
using MongoDB.Driver;
using Sample.DataSource.Extensibility;
using Sample.DataSource.Extensibility.Data;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.DataSource.Repositories;

namespace Sample.DataSource.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly IDataStore dataStore;

        public AccountRepository(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public IEnumerable<IAccount> GetAccounts()
        {
            return dataStore.Accounts.AsQueryable();
        }
    }
}