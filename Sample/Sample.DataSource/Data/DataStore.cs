using System.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Sample.DataSource.Entities;
using Sample.DataSource.Extensibility.Data;
using Sample.Extensibility.DataSource.Entities;

namespace Sample.DataSource.Data
{
    internal class DataStore : IDataStore
    {
        private const string DatabaseName = "sample";
        private const string AccountCollectionName = "accounts";
        private const string IncomingPaymentsCollectionName = "incomingPayments";
        private const string OutgoingPaymentsCollectionName = "outgoingPayments";
        private const string IncomingTaxesCollectionName = "incomingTaxes";
        private const string OutgoingTaxesCollectionName = "outgoingTaxes";

        private readonly IDataGenerator dataGenerator;

        public DataStore(IDataGenerator dataGenerator)
        {
            this.dataGenerator = dataGenerator;
            var mongoClient = new MongoClient();
            IMongoDatabase mongoDb = mongoClient.GetDatabase(DatabaseName);
            Accounts = mongoDb.GetCollection<IAccount>(AccountCollectionName);
            IncomingPayments = mongoDb.GetCollection<IIncomingPayment>(IncomingPaymentsCollectionName);
            OutgoingPayments = mongoDb.GetCollection<IOutgoingPayment>(OutgoingPaymentsCollectionName);
            IncomingTaxes = mongoDb.GetCollection<IIncomingTax>(IncomingTaxesCollectionName);
            OutgoingTaxes = mongoDb.GetCollection<IOutgoingTax>(OutgoingTaxesCollectionName);
            RegisterClassMaps();
            Initialize();
        }

        public IMongoCollection<IAccount> Accounts { get; }

        public IMongoCollection<IIncomingPayment> IncomingPayments { get; }

        public IMongoCollection<IOutgoingPayment> OutgoingPayments { get; }

        public IMongoCollection<IIncomingTax> IncomingTaxes { get; }

        public IMongoCollection<IOutgoingTax> OutgoingTaxes { get; }

        private static void RegisterClassMaps()
        {
            BsonClassMap.RegisterClassMap<Account>(map => map.AutoMap());
            BsonClassMap.RegisterClassMap<IncomingPayment>(map => map.AutoMap());
            BsonClassMap.RegisterClassMap<OutgoingPayment>(map => map.AutoMap());
            BsonClassMap.RegisterClassMap<IncomingTax>(map => map.AutoMap());
            BsonClassMap.RegisterClassMap<OutgoingTax>(map => map.AutoMap());
        }

        private static WriteModel<TEntity> CreateInsertModel<TEntity>(TEntity entity)
        {
            return new InsertOneModel<TEntity>(entity);
        }

        private void Initialize()
        {
            if (Accounts.Count(account => true) == 0)
            {
                DataContainer data = dataGenerator.GenerateData();
                Accounts.BulkWrite(data.Accounts.Select(CreateInsertModel));
                IncomingPayments.BulkWrite(data.IncomingPayments.Select(CreateInsertModel));
                OutgoingPayments.BulkWrite(data.OutgoingPayments.Select(CreateInsertModel));
                IncomingTaxes.BulkWrite(data.IncomingTaxes.Select(CreateInsertModel));
                OutgoingTaxes.BulkWrite(data.OutgoingTaxes.Select(CreateInsertModel));
            }
        }
    }
}