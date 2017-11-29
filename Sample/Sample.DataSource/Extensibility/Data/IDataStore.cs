using MongoDB.Driver;
using Sample.Extensibility.DataSource.Entities;

namespace Sample.DataSource.Extensibility.Data
{
    internal interface IDataStore
    {
        IMongoCollection<IAccount> Accounts { get; }

        IMongoCollection<IIncomingPayment> IncomingPayments { get; }

        IMongoCollection<IOutgoingPayment> OutgoingPayments { get; }

        IMongoCollection<IIncomingTax> IncomingTaxes { get; }

        IMongoCollection<IOutgoingTax> OutgoingTaxes { get; }
    }
}