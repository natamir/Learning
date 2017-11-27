using System.Collections.Generic;

namespace Sample.Extensibility.Domain.Filters
{
    public interface ITaxFilter : IDateFilter
    {
        IEnumerable<int> PaymentIds { get; }
    }
}