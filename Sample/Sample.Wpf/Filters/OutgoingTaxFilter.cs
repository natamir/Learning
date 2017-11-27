using System;
using System.Collections.Generic;
using Sample.Extensibility.DataSource.Enums;
using Sample.Extensibility.Domain.Filters;

namespace Sample.Wpf.Filters
{
    internal class OutgoingTaxFilter : IOutgoingTaxFilter
    {
        public IEnumerable<int> PaymentIds { get; set; }

        public IEnumerable<OutgoingTaxType> Types { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}