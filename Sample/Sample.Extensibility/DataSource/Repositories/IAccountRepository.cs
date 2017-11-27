using System.Collections.Generic;
using Sample.Extensibility.DataSource.Entities;

namespace Sample.Extensibility.DataSource.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<IAccount> GetAccounts();
    }
}