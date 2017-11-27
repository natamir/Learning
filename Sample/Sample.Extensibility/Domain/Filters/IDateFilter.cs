using System;

namespace Sample.Extensibility.Domain.Filters
{
    public interface IDateFilter
    {
        DateTime FromDate { get; }

        DateTime ToDate { get; }
    }
}