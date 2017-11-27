﻿using System.Collections.Generic;
using Sample.Extensibility.DataSource.Enums;

namespace Sample.Extensibility.Domain.Filters
{
    public interface IIncomingPaymentFilter : IPaymentFilter
    {
        IEnumerable<IncomingPaymentType> Types { get; }
    }
}