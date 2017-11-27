using System.Collections.Generic;
using Sample.Extensibility.DataSource.Enums;

namespace Sample.Extensibility.Domain.Filters
{
    public interface IOutgoingTaxFilter : ITaxFilter
    {
        IEnumerable<OutgoingTaxType> Types { get; }
    }
}