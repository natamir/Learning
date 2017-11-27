using System.Collections.Generic;
using Sample.Extensibility.DataSource.Enums;

namespace Sample.Extensibility.Domain.Filters
{
    public interface IIncomingTaxFilter : ITaxFilter
    {
        IEnumerable<IncomingTaxType> Types { get; }
    }
}