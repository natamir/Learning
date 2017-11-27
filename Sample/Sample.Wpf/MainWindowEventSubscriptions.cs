using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Controls;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain.Filters;
using Sample.Wpf.Data;
using Sample.Wpf.Extensibility.Data;

namespace Sample.Wpf
{
    public partial class MainWindow
    {
        private void InitializeEventSubscriptions()
        {
            IObservable<EventPattern<SelectionChangedEventArgs>> accountSelectionChangedObservable = CreateAccountSelectionChangedObservable();

            CreateIncomingPaymentsFilterObservable(accountSelectionChangedObservable)
                .Do(incomingPaymentsFilterEvent =>
                {
                    IObservable<IncomingPaymentsResult> incomingPaymentsObservable = CreateIncomingPaymentsObservable();
                    SubscribeIncomingPaymentsChartUpdate(incomingPaymentsObservable);

                    IObservable<IncomingTaxesResult> incomingTaxesObservable = CreateIncomingTaxesObservable(incomingPaymentsObservable);
                    SubscribeIncomingTaxesChartUpdate(incomingTaxesObservable);

                    IObservable<OutgoingPaymentsResult> outgoingPaymentsObservable = CreateOutgoingPaymentsObservable();
                    IObservable<OutgoingTaxesResult> outgoingTaxesObservable = CreateOutgoingTaxesObservable(outgoingPaymentsObservable);
                    SubscribeTotalTaxesChartUpdate(incomingTaxesObservable, outgoingTaxesObservable);
                })
                .RunAsync(CancellationToken.None);

            CreateOutgoingPaymentsFilterObservable(accountSelectionChangedObservable)
                .Do(outgoingPaymentsFilterEvent =>
                {
                    IObservable<OutgoingPaymentsResult> outgoingPaymentsObservable = CreateOutgoingPaymentsObservable();
                    SubscribeOutgoingPaymentsChartUpdate(outgoingPaymentsObservable);

                    IObservable<OutgoingTaxesResult> outgoingTaxesObservable = CreateOutgoingTaxesObservable(outgoingPaymentsObservable);
                    SubscribeOutgoingTaxesChartObservable(outgoingTaxesObservable);

                    IObservable<IncomingPaymentsResult> incomingPaymentsObservable = CreateIncomingPaymentsObservable();
                    IObservable<IncomingTaxesResult> incomingTaxesObservable = CreateIncomingTaxesObservable(incomingPaymentsObservable);
                    SubscribeTotalTaxesChartUpdate(incomingTaxesObservable, outgoingTaxesObservable);
                })
                .RunAsync(CancellationToken.None);

            CreateIncomingTaxesFilterObservable()
                .Do(incomingTaxesFilterEvent =>
                {
                    IObservable<IncomingTaxesResult> incomingTaxesObservable = CreateIncomingTaxesObservable();
                    SubscribeIncomingTaxesChartUpdate(incomingTaxesObservable);

                    IObservable<OutgoingTaxesResult> outgoingTaxesObservable = CreateOutgoingTaxesObservable();
                    SubscribeTotalTaxesChartUpdate(incomingTaxesObservable, outgoingTaxesObservable);
                })
                .RunAsync(CancellationToken.None);

            CreateOutgoingTaxesFilterObservable()
                .Do(outgoingTaxesFilterEvent =>
                {
                    IObservable<OutgoingTaxesResult> outgoingTaxesObservable = CreateOutgoingTaxesObservable();
                    SubscribeOutgoingTaxesChartObservable(outgoingTaxesObservable);

                    IObservable<IncomingTaxesResult> incomingTaxesObservable = CreateIncomingTaxesObservable();
                    SubscribeTotalTaxesChartUpdate(incomingTaxesObservable, outgoingTaxesObservable);
                })
                .RunAsync(CancellationToken.None);
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateIncomingPaymentsFilterObservable(IObservable<EventPattern<SelectionChangedEventArgs>> accountSelectionChangedObservable)
        {
            return accountSelectionChangedObservable
                .Merge(CreateIncomingPaymentTypeSelectionChangedObservable())
                .Merge(CreateIncomingPaymentFromDateSelectionChangedObservable())
                .Merge(CreateIncomingPaymentToDateSelectionChangedObservable());
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateOutgoingPaymentsFilterObservable(IObservable<EventPattern<SelectionChangedEventArgs>> accountSelectionChangedObservable)
        {
            return accountSelectionChangedObservable
                .Merge(CreateOutgoingPaymentTypeSelectionChangedObservable())
                .Merge(CreateOutgoingPaymentFromDateSelectionChangedObservable())
                .Merge(CreateOutgoingPaymentToDateSelectionChangedObservable());
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateIncomingTaxesFilterObservable()
        {
            return CreateIncomingTaxTypeSelectionChangedObservable();
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateOutgoingTaxesFilterObservable()
        {
            return CreateOutgoingTaxTypeSelectionChangedObservable();
        }

        private IObservable<IncomingPaymentsResult> CreateIncomingPaymentsObservable()
        {
            return Observable.Return(GetSelectedAccountIds())
                .Select(accountIds =>
                {
                    IIncomingPaymentFilter incomingPaymentFilter = CreateIncomingPaymentFilter(accountIds);
                    IReadOnlyCollection<IIncomingPayment> incomingPayments = incomingPaymentRepository.GetIncomingPayments(incomingPaymentFilter).ToList();
                    return new IncomingPaymentsResult(incomingPaymentFilter, incomingPayments);
                });
        }

        private IObservable<OutgoingPaymentsResult> CreateOutgoingPaymentsObservable()
        {
            return Observable.Return(GetSelectedAccountIds())
                .Select(accountIds =>
                {
                    IOutgoingPaymentFilter outgoingPaymentFilter = CreateOutcomingPaymentFilter(accountIds);
                    IReadOnlyCollection<IOutgoingPayment> outgoingPayments = outgoingPaymentRepository.GetOutgoingPayments(outgoingPaymentFilter).ToList();
                    return new OutgoingPaymentsResult(outgoingPaymentFilter, outgoingPayments);
                });
        }

        private IObservable<IncomingTaxesResult> CreateIncomingTaxesObservable()
        {
            return Observable.Return(GetSelectedIds(incomingPaymentIdList))
                .Select(paymentIds =>
                {
                    IIncomingTaxFilter incomingTaxFilter = CreateIncomingTaxFilter(paymentIds);
                    IReadOnlyCollection<IIncomingTax> incomingTaxes = incomingTaxRepository.GetIncomingTaxes(incomingTaxFilter).ToList();
                    return new IncomingTaxesResult(incomingTaxFilter, incomingTaxes);
                });
        }

        private IObservable<IncomingTaxesResult> CreateIncomingTaxesObservable(IObservable<IncomingPaymentsResult> incomingPaymentsObservable)
        {
            return incomingPaymentsObservable
                .Select(paymentsResult =>
                {
                    IIncomingTaxFilter incomingTaxFilter = CreateIncomingTaxFilter(paymentsResult.IncomingPayments.Select(payment => payment.Id));
                    IReadOnlyCollection<IIncomingTax> incomingTaxes = incomingTaxRepository.GetIncomingTaxes(incomingTaxFilter).ToList();
                    return new IncomingTaxesResult(incomingTaxFilter, incomingTaxes);
                });
        }

        private IObservable<OutgoingTaxesResult> CreateOutgoingTaxesObservable()
        {
            return Observable.Return(GetSelectedIds(outgoingPaymentIdList))
                .Select(paymentIds =>
                {
                    IOutgoingTaxFilter outgoingTaxFilter = CreateOutgoingTaxFilter(paymentIds);
                    IReadOnlyCollection<IOutgoingTax> outgoingTaxes = outgoingTaxRepository.GetOutgoingTaxes(outgoingTaxFilter).ToList();
                    return new OutgoingTaxesResult(outgoingTaxFilter, outgoingTaxes);
                });
        }

        private IObservable<OutgoingTaxesResult> CreateOutgoingTaxesObservable(IObservable<OutgoingPaymentsResult> outgoingPaymentsObservable)
        {
            return outgoingPaymentsObservable
                .Select(paymentsResult =>
                {
                    IOutgoingTaxFilter outgoingTaxFilter = CreateOutgoingTaxFilter(paymentsResult.OutgoingPayments.Select(payment => payment.Id));
                    IReadOnlyCollection<IOutgoingTax> outgoingTaxes = outgoingTaxRepository.GetOutgoingTaxes(outgoingTaxFilter).ToList();
                    return new OutgoingTaxesResult(outgoingTaxFilter, outgoingTaxes);
                });
        }

        private void SubscribeIncomingPaymentsChartUpdate(IObservable<IncomingPaymentsResult> incomingPaymentsObservable)
        {
            IObservable<IChartPairData> incomingPaymentsDataObservable =
                incomingPaymentsObservable.Select(incomingPaymentsResult => incomingPaymentChartDataProvider.GetChartData(incomingPaymentsResult.IncomingPayments, incomingPaymentsResult.IncomingPaymentFilter));
            incomingPaymentsDataObservable.Do(incomingPaymentsChartData => InitializeIdList(incomingPaymentsChartData, incomingPaymentIdList)).RunAsync(CancellationToken.None);
            incomingPaymentsDataObservable.Do(incomingPaymentsChartData => pieChartInitializer.Initialize(incomingPaymentsPieChart, incomingPaymentsChartData.AmountPercentData)).RunAsync(CancellationToken.None);
            incomingPaymentsDataObservable.Do(incomingPaymentsChartData => barChartInitializer.Initialize(incomingPaymentsBarChart, incomingPaymentsChartData.AmountDateData)).RunAsync(CancellationToken.None);
        }

        private void SubscribeOutgoingPaymentsChartUpdate(IObservable<OutgoingPaymentsResult> outgoingPaymentsObservable)
        {
            IObservable<IChartPairData> outgoingPaymentsDataObservable =
                outgoingPaymentsObservable.Select(outgoingPaymentResult => outgoingPaymentChartDataProvider.GetChartData(outgoingPaymentResult.OutgoingPayments, outgoingPaymentResult.OutgoingPaymentFilter));
            outgoingPaymentsDataObservable.Do(outgoingPaymentsChartData => InitializeIdList(outgoingPaymentsChartData, outgoingPaymentIdList)).RunAsync(CancellationToken.None);
            outgoingPaymentsDataObservable.Do(outgoingPaymentsChartData => pieChartInitializer.Initialize(outgoingPaymentsPieChart, outgoingPaymentsChartData.AmountPercentData)).RunAsync(CancellationToken.None);
            outgoingPaymentsDataObservable.Do(outgoingPaymentsChartData => barChartInitializer.Initialize(outgoingPaymentsBarChart, outgoingPaymentsChartData.AmountDateData)).RunAsync(CancellationToken.None);
        }

        private void SubscribeIncomingTaxesChartUpdate(IObservable<IncomingTaxesResult> incomingTaxesObservable)
        {
            IObservable<IChartPairData> incomingTaxesDataObservable =
                incomingTaxesObservable.Select(incomingTaxesResult => incomingTaxChartDataProvider.GetChartData(incomingTaxesResult.IncomingTaxes, incomingTaxesResult.IncomingTaxFilter));
            incomingTaxesDataObservable.Do(incomingTaxChartData => InitializeIdList(incomingTaxChartData, incomingTaxIdList)).RunAsync(CancellationToken.None);
            incomingTaxesDataObservable.Do(incomingTaxChartData => pieChartInitializer.Initialize(incomingTaxesPieChart, incomingTaxChartData.AmountPercentData)).RunAsync(CancellationToken.None);
            incomingTaxesDataObservable.Do(incomingTaxChartData => barChartInitializer.Initialize(incomingTaxesBarChart, incomingTaxChartData.AmountDateData)).RunAsync(CancellationToken.None);
        }

        private void SubscribeOutgoingTaxesChartObservable(IObservable<OutgoingTaxesResult> outgoingTaxesObservable)
        {
            IObservable<IChartPairData> outgoingTaxesDataObservable =
                outgoingTaxesObservable.Select(outgoingTaxesResult => outgoingTaxChartDataProvider.GetChartData(outgoingTaxesResult.OutgoingTaxes, outgoingTaxesResult.OutgoingTaxFilter));
            outgoingTaxesDataObservable.Do(outgoingTaxChartData => InitializeIdList(outgoingTaxChartData, outgoingTaxIdList)).RunAsync(CancellationToken.None);
            outgoingTaxesDataObservable.Do(outgoingTaxChartData => pieChartInitializer.Initialize(outgoingTaxesPieChart, outgoingTaxChartData.AmountPercentData)).RunAsync(CancellationToken.None);
            outgoingTaxesDataObservable.Do(outgoingTaxChartData => barChartInitializer.Initialize(outgoingTaxesBarChart, outgoingTaxChartData.AmountDateData)).RunAsync(CancellationToken.None);
        }

        private void SubscribeTotalTaxesChartUpdate(IObservable<IncomingTaxesResult> incomingTaxesObservable, IObservable<OutgoingTaxesResult> outgoingTaxesObservable)
        {
            IObservable<IChartPairData> totalTaxesDataObservable = incomingTaxesObservable
                .CombineLatest(outgoingTaxesObservable, (incomingTaxesResult, outgoingTaxesResult) => totalTaxChartDataProvider.GetChartData(incomingTaxesResult.IncomingTaxes, outgoingTaxesResult.OutgoingTaxes));
            totalTaxesDataObservable.Do(totalTaxChartData => pieChartInitializer.Initialize(totalTaxesPieChart, totalTaxChartData.AmountPercentData)).RunAsync(CancellationToken.None);
            totalTaxesDataObservable.Do(totalTaxChartData => barChartInitializer.Initialize(totalTaxesBarChart, totalTaxChartData.AmountDateData)).RunAsync(CancellationToken.None);
        }
    }
}