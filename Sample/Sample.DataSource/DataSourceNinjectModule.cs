﻿using Ninject.Modules;
using Sample.DataSource.Data;
using Sample.DataSource.Extensibility.Data;
using Sample.DataSource.Repositories;
using Sample.Extensibility.DataSource.Repositories;

namespace Sample.DataSource
{
    public class DataSourceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataGenerator>().To<DataGenerator>();
            Bind<IDataStore>().To<DataStore>().InSingletonScope();
            Bind<IAccountRepository>().To<AccountRepository>();
            Bind<IIncomingPaymentRepository>().To<IncomingPaymentRepository>();
            Bind<IOutgoingPaymentRepository>().To<OutgoingPaymentRepository>();
            Bind<IIncomingTaxRepository>().To<IncomingTaxRepository>();
            Bind<IOutgoingTaxRepository>().To<OutgoingTaxRepository>();
        }
    }
}