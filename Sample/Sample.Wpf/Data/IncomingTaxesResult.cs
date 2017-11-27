using System.Collections.Generic;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;

namespace Sample.Wpf.Data
{
    internal class IncomingTaxesResult
    {
        public IncomingTaxesResult(IIncomingTaxFilter incomingTaxFilter, IReadOnlyCollection<IIncomingTax> incomingTaxes)
        {
            IncomingTaxFilter = incomingTaxFilter;
            IncomingTaxes = incomingTaxes;
        }

        public IIncomingTaxFilter IncomingTaxFilter { get; }

        public IReadOnlyCollection<IIncomingTax> IncomingTaxes { get; }
    }
}