using System;
using System.Collections.Generic;
using Sample.Extensibility.DataSource.Enums;
using Sample.Extensibility.Domain.Filters;

namespace Sample.Wpf.Filters
{
    internal class IncomingTaxFilter : IIncomingTaxFilter
    {
        public IEnumerable<int> PaymentIds { get; set; }

        public IEnumerable<IncomingTaxType> Types { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}