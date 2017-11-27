using System;
using System.Collections.Generic;
using Sample.Extensibility.DataSource.Enums;
using Sample.Extensibility.Domain.Filters;

namespace Sample.Wpf.Filters
{
    internal class OutgoingPaymentFilter : IOutgoingPaymentFilter
    {
        public IEnumerable<int> AccountIds { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public IEnumerable<OutgoingPaymentType> Types { get; set; }
    }
}