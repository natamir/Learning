using Ninject.Modules;
using Sample.DataSource.Extensibility;
using Sample.DataSource.Generator;
using Sample.DataSource.Repositories;
using Sample.Extensibility.DataSource.Repositories;

namespace Sample.DataSource
{
    public class DataSourceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataStore>().To<DataStore>();
            Bind<IAccountRepository>().To<AccountRepository>();
            Bind<IIncomingPaymentRepository>().To<IncomingPaymentRepository>();
            Bind<IOutgoingPaymentRepository>().To<OutgoingPaymentRepository>();
            Bind<IIncomingTaxRepository>().To<IncomingTaxRepository>();
            Bind<IOutgoingTaxRepository>().To<OutgoingTaxRepository>();
        }
    }
}