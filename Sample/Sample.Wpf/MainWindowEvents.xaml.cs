using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Controls;

namespace Sample.Wpf
{
    public partial class MainWindow
    {
        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateAccountSelectionChangedObservable()
        {
            return Observable.FromEventPattern<SelectionChangedEventHandler, SelectionChangedEventArgs>(
                handler => accountList.SelectionChanged += handler,
                handler => accountList.SelectionChanged -= handler);
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateIncomingPaymentTypeSelectionChangedObservable()
        {
            return Observable.FromEventPattern<SelectionChangedEventHandler, SelectionChangedEventArgs>(
                handler => incomingPaymentTypeList.SelectionChanged += handler,
                handler => incomingPaymentTypeList.SelectionChanged -= handler);
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateIncomingPaymentFromDateSelectionChangedObservable()
        {
            return Observable.FromEventPattern<SelectionChangedEventArgs>(
                handler => incomingPaymentFromDate.SelectedDateChanged += handler,
                handler => incomingPaymentFromDate.SelectedDateChanged -= handler);
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateIncomingPaymentToDateSelectionChangedObservable()
        {
            return Observable.FromEventPattern<SelectionChangedEventArgs>(
                handler => incomingPaymentToDate.SelectedDateChanged += handler,
                handler => incomingPaymentToDate.SelectedDateChanged -= handler);
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateOutgoingPaymentTypeSelectionChangedObservable()
        {
            return Observable.FromEventPattern<SelectionChangedEventHandler, SelectionChangedEventArgs>(
                handler => outgoingPaymentTypeList.SelectionChanged += handler,
                handler => outgoingPaymentTypeList.SelectionChanged -= handler);
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateOutgoingPaymentFromDateSelectionChangedObservable()
        {
            return Observable.FromEventPattern<SelectionChangedEventArgs>(
                handler => outgoingPaymentFromDate.SelectedDateChanged += handler,
                handler => outgoingPaymentFromDate.SelectedDateChanged -= handler);
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateOutgoingPaymentToDateSelectionChangedObservable()
        {
            return Observable.FromEventPattern<SelectionChangedEventArgs>(
                handler => outgoingPaymentToDate.SelectedDateChanged += handler,
                handler => outgoingPaymentToDate.SelectedDateChanged -= handler);
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateIncomingTaxTypeSelectionChangedObservable()
        {
            return Observable.FromEventPattern<SelectionChangedEventHandler, SelectionChangedEventArgs>(
                handler => incomingTaxTypeList.SelectionChanged += handler,
                handler => incomingTaxTypeList.SelectionChanged -= handler);
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> CreateOutgoingTaxTypeSelectionChangedObservable()
        {
            return Observable.FromEventPattern<SelectionChangedEventHandler, SelectionChangedEventArgs>(
                handler => outgoingTaxTypeList.SelectionChanged += handler,
                handler => outgoingTaxTypeList.SelectionChanged -= handler);
        }
    }
}