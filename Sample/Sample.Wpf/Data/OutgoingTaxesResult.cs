using System.Collections.Generic;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;

namespace Sample.Wpf.Data
{
    internal class OutgoingTaxesResult
    {
        public OutgoingTaxesResult(IOutgoingTaxFilter outgoingTaxFilter, IReadOnlyCollection<IOutgoingTax> outgoingTaxes)
        {
            OutgoingTaxFilter = outgoingTaxFilter;
            OutgoingTaxes = outgoingTaxes;
        }

        public IOutgoingTaxFilter OutgoingTaxFilter { get; }

        public IReadOnlyCollection<IOutgoingTax> OutgoingTaxes { get; set; }
    }
}