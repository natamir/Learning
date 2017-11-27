using System.Collections.Generic;
using Sample.Extensibility.DataSource.Entities;

namespace Sample.Extensibility.Domain
{
    public interface ICalculationService
    {
        IEnumerable<IAccount> Calculate();
    }
}