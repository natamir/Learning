using System.Collections.Generic;
using Sample.DataSource.Extensibility;
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
            return dataStore.Accounts;
        }
    }
}