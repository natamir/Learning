using Ninject.Modules;
using Sample.Wpf.Extensibility.Initializers;
using Sample.Wpf.Extensibility.Providers;
using Sample.Wpf.Initializers;
using Sample.Wpf.Providers;

namespace Sample.Wpf
{
    public class DefaultNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IIncomingPaymentChartDataProvider>().To<IncomingPaymentChartDataProvider>();
            Bind<IOutgoingPaymentChartDataProvider>().To<OutgoingPaymentChartDataProvider>();
            Bind<IIncomingTaxChartDataProvider>().To<IncomingTaxChartDataProvider>();
            Bind<IOutgoingTaxChartDataProvider>().To<OutgoingTaxChartDataProvider>();
            Bind<ITotalTaxChartDataProvider>().To<TotalTaxChartDataProvider>();
            Bind<IPieChartInitializer>().To<PieChartInitializer>();
            Bind<IBarChartInitializer>().To<BarChartInitializer>();
        }
    }
}