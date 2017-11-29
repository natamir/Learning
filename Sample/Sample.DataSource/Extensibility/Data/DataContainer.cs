using System.Collections.Generic;
using Sample.Extensibility.DataSource.Entities;

namespace Sample.DataSource.Extensibility.Data
{
    internal class DataContainer
    {
        public DataContainer(
            IReadOnlyCollection<IAccount> accounts,
            IReadOnlyCollection<IIncomingPayment> incomingPayments,
            IReadOnlyCollection<IOutgoingPayment> outgoingPayments,
            IReadOnlyCollection<IIncomingTax> incomingTaxes,
            IReadOnlyCollection<IOutgoingTax> outgoingTaxes)
        {
            Accounts = accounts;
            IncomingPayments = incomingPayments;
            OutgoingPayments = outgoingPayments;
            IncomingTaxes = incomingTaxes;
            OutgoingTaxes = outgoingTaxes;
        }

        public IReadOnlyCollection<IAccount> Accounts { get; }

        public IReadOnlyCollection<IIncomingPayment> IncomingPayments { get; }

        public IReadOnlyCollection<IOutgoingPayment> OutgoingPayments { get; }

        public IReadOnlyCollection<IIncomingTax> IncomingTaxes { get; }

        public IReadOnlyCollection<IOutgoingTax> OutgoingTaxes { get; }
    }
}