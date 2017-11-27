using System;
using System.Collections.Generic;

namespace Sample.Extensibility.Domain.Filters
{
    public interface IPaymentFilter : IDateFilter
    {
        IEnumerable<int> AccountIds { get; }
    }
}