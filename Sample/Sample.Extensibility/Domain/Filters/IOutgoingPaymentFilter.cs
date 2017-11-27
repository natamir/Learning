using System.Collections.Generic;
using Sample.Extensibility.DataSource.Enums;

namespace Sample.Extensibility.Domain.Filters
{
    public interface IOutgoingPaymentFilter : IPaymentFilter
    {
        IEnumerable<OutgoingPaymentType> Types { get; }
    }
}