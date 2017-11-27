using System.Collections.Generic;
using Sample.Extensibility.DataSource.Entities;

namespace Sample.DataSource.Extensibility
{
    internal interface IDataStore
    {
        IEnumerable<IAccount> Accounts { get; }

        IEnumerable<IIncomingPayment> IncomingPayments { get; }

        IEnumerable<IOutgoingPayment> OutgoingPayments { get; }

        IEnumerable<IIncomingTax> IncomingTaxes { get; }

        IEnumerable<IOutgoingTax> OutgoingTaxes { get; }
    }
}