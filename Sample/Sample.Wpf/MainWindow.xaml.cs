using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.DataSource.Enums;
using Sample.Extensibility.DataSource.Repositories;
using Sample.Extensibility.Domain.Filters;
using Sample.Wpf.Extensibility.Data;
using Sample.Wpf.Extensibility.Initializers;
using Sample.Wpf.Extensibility.Providers;
using Sample.Wpf.Filters;

namespace Sample.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IAccountRepository accountRepository;
        private readonly IIncomingPaymentRepository incomingPaymentRepository;
        private readonly IOutgoingPaymentRepository outgoingPaymentRepository;
        private readonly IIncomingTaxRepository incomingTaxRepository;
        private readonly IOutgoingTaxRepository outgoingTaxRepository;
        private readonly IIncomingPaymentChartDataProvider incomingPaymentChartDataProvider;
        private readonly IOutgoingPaymentChartDataProvider outgoingPaymentChartDataProvider;
        private readonly IIncomingTaxChartDataProvider incomingTaxChartDataProvider;
        private readonly IOutgoingTaxChartDataProvider outgoingTaxChartDataProvider;
        private readonly IPieChartInitializer pieChartInitializer;
        private readonly IBarChartInitializer barChartInitializer;
        private readonly ITotalTaxChartDataProvider totalTaxChartDataProvider;

        public MainWindow(
            IAccountRepository accountRepository,
            IIncomingPaymentRepository incomingPaymentRepository,
            IOutgoingPaymentRepository outgoingPaymentRepository,
            IIncomingTaxRepository incomingTaxRepository,
            IOutgoingTaxRepository outgoingTaxRepository,
            IIncomingPaymentChartDataProvider incomingPaymentChartDataProvider,
            IOutgoingPaymentChartDataProvider outgoingPaymentChartDataProvider,
            IIncomingTaxChartDataProvider incomingTaxChartDataProvider,
            IOutgoingTaxChartDataProvider outgoingTaxChartDataProvider,
            IPieChartInitializer pieChartInitializer,
            IBarChartInitializer barChartInitializer,
            ITotalTaxChartDataProvider totalTaxChartDataProvider)
        {
            this.accountRepository = accountRepository;
            this.incomingPaymentRepository = incomingPaymentRepository;
            this.outgoingPaymentRepository = outgoingPaymentRepository;
            this.incomingTaxRepository = incomingTaxRepository;
            this.outgoingTaxRepository = outgoingTaxRepository;
            this.incomingPaymentChartDataProvider = incomingPaymentChartDataProvider;
            this.outgoingPaymentChartDataProvider = outgoingPaymentChartDataProvider;
            this.incomingTaxChartDataProvider = incomingTaxChartDataProvider;
            this.outgoingTaxChartDataProvider = outgoingTaxChartDataProvider;
            this.pieChartInitializer = pieChartInitializer;
            this.barChartInitializer = barChartInitializer;
            this.totalTaxChartDataProvider = totalTaxChartDataProvider;
            InitializeComponent();
            InitializeCaptions();
            InitializeAccountList();
            InitializeIncomingPaymentFilter();
            InitializeOutgoingPaymentFilter();
            InitializeIncomingTaxFilter();
            InitializeOutgoingTaxFilter();
            InitializeEventSubscriptions();
        }

        private static IEnumerable<TType> GetSelectedTypes<TType>(ListBox typeList)
        {
            return typeList.SelectedItems.Cast<string>().Select(item => (TType)Enum.Parse(typeof(TType), item));
        }

        private static void InitializeIdList(IChartPairData chartData, ItemsControl idList)
        {
            idList.Items.Clear();
            foreach (int id in chartData.Ids)
            {
                idList.Items.Add(id);
            }
        }

        private static IReadOnlyCollection<int> GetSelectedIds(ItemsControl idList)
        {
            return idList.Items.Cast<int>().ToList();
        }

        private IReadOnlyCollection<int> GetSelectedAccountIds()
        {
            return accountList.SelectedItems.Cast<ListBoxItem>().Select(item => (int)item.Tag).ToList();
        }

        private void InitializeCaptions()
        {
            accountsLabel.Content = "Accounts:";
        }

        private void InitializeAccountList()
        {
            foreach (IAccount account in accountRepository.GetAccounts())
            {
                accountList.Items.Add(new ListBoxItem { Content = account.Number, Tag = account.Id });
            }
        }

        private void InitializeIncomingPaymentFilter()
        {
            foreach (string type in Enum.GetNames(typeof(IncomingPaymentType)))
            {
                incomingPaymentTypeList.Items.Add(type);
            }
            incomingPaymentTypeList.SelectAll();
            incomingPaymentFromDate.SelectedDate = DateTime.Today.AddYears(-1);
            incomingPaymentToDate.SelectedDate = DateTime.Today;
        }

        private void InitializeOutgoingPaymentFilter()
        {
            foreach (string type in Enum.GetNames(typeof(OutgoingPaymentType)))
            {
                outgoingPaymentTypeList.Items.Add(type);
            }
            outgoingPaymentTypeList.SelectAll();
            outgoingPaymentFromDate.SelectedDate = DateTime.Today.AddYears(-1);
            outgoingPaymentToDate.SelectedDate = DateTime.Today;
        }

        private void InitializeIncomingTaxFilter()
        {
            foreach (string type in Enum.GetNames(typeof(IncomingTaxType)))
            {
                incomingTaxTypeList.Items.Add(type);
            }
            incomingTaxTypeList.SelectAll();
        }

        private void InitializeOutgoingTaxFilter()
        {
            foreach (string type in Enum.GetNames(typeof(OutgoingTaxType)))
            {
                outgoingTaxTypeList.Items.Add(type);
            }
            outgoingTaxTypeList.SelectAll();
        }

        private IIncomingPaymentFilter CreateIncomingPaymentFilter(IReadOnlyCollection<int> accountIds)
        {
            return new IncomingPaymentFilter
            {
                AccountIds = accountIds,
                Types = GetSelectedTypes<IncomingPaymentType>(incomingPaymentTypeList),
                FromDate = incomingPaymentFromDate.SelectedDate ?? DateTime.MinValue,
                ToDate = incomingPaymentToDate.SelectedDate ?? DateTime.Now
            };
        }

        private IOutgoingPaymentFilter CreateOutcomingPaymentFilter(IEnumerable<int> accountIds)
        {
            return new OutgoingPaymentFilter
            {
                AccountIds = accountIds,
                Types = GetSelectedTypes<OutgoingPaymentType>(outgoingPaymentTypeList),
                FromDate = incomingPaymentFromDate.SelectedDate ?? DateTime.MinValue,
                ToDate = incomingPaymentToDate.SelectedDate ?? DateTime.Now
            };
        }

        private IIncomingTaxFilter CreateIncomingTaxFilter(IEnumerable<int> paymentIds)
        {
            return new IncomingTaxFilter
            {
                PaymentIds = paymentIds,
                Types = GetSelectedTypes<IncomingTaxType>(incomingTaxTypeList),
                FromDate = incomingPaymentFromDate.SelectedDate ?? DateTime.MinValue,
                ToDate = incomingPaymentToDate.SelectedDate ?? DateTime.Now
            };
        }

        private IOutgoingTaxFilter CreateOutgoingTaxFilter(IEnumerable<int> paymentIds)
        {
            return new OutgoingTaxFilter
            {
                PaymentIds = paymentIds,
                Types = GetSelectedTypes<OutgoingTaxType>(outgoingTaxTypeList),
                FromDate = incomingPaymentFromDate.SelectedDate ?? DateTime.MinValue,
                ToDate = incomingPaymentToDate.SelectedDate ?? DateTime.Now
            };
        }
    }
}