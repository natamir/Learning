using System;
using System.Collections.Generic;
using System.Linq;
using Sample.DataSource.Entities;
using Sample.DataSource.Extensibility.Data;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.DataSource.Enums;

namespace Sample.DataSource.Data
{
    internal class DataGenerator : IDataGenerator
    {
        private const int AccountRecordCount = 20;
        private const int PaymentRecordCount = 10;
        private const int TaxRecordCount = 10;
        private const int AccountNumberGroupCount = 4;
        private const int AccountNumberInGroupCount = 4;
        private const int MaxAccountNumber = 9;
        private const int MaxTaxValue = 20;
        private const int MaxAmountValue = 100000;
        private const int DaysInYear = 365;

        private readonly Random random = new Random();

        public DataContainer GenerateData()
        {
            IReadOnlyCollection<IAccount> accounts = CreateAccounts().ToList();
            IReadOnlyCollection<IIncomingPayment> incomingPayments = CreateIncomingPayments(accounts).ToList();
            IReadOnlyCollection<IOutgoingPayment> outgoingPayments = CreateOutgoingPayments(accounts).ToList();
            IReadOnlyCollection<IIncomingTax> incomingTaxes = CreateIncomingTaxes(incomingPayments).ToList();
            IReadOnlyCollection<IOutgoingTax> outgoingTaxes = CreateOutgoingTaxes(outgoingPayments).ToList();
            return new DataContainer(accounts, incomingPayments, outgoingPayments, incomingTaxes, outgoingTaxes);
        }

        private static IEnumerable<int> CreateIdentifiers(int parentIdentifier, int recordCount)
        {
            return Enumerable.Range(parentIdentifier * recordCount, recordCount);
        }

        private IEnumerable<IAccount> CreateAccounts()
        {
            return CreateIdentifiers(0, AccountRecordCount).Select(CreateAccount);
        }

        private IEnumerable<IIncomingPayment> CreateIncomingPayments(IEnumerable<IAccount> accounts)
        {
            return accounts.SelectMany(account => CreateIdentifiers(account.Id, PaymentRecordCount).Select(id => CreateIncomingPayment(id, account)));
        }

        private IEnumerable<IOutgoingPayment> CreateOutgoingPayments(IEnumerable<IAccount> accounts)
        {
            return accounts.SelectMany(account => CreateIdentifiers(account.Id, PaymentRecordCount).Select(id => CreateOutgoingPayment(id, account)));
        }

        private IEnumerable<IIncomingTax> CreateIncomingTaxes(IEnumerable<IIncomingPayment> incomingPayments)
        {
            return incomingPayments.SelectMany(incomingPayment => CreateIdentifiers(incomingPayment.Id, TaxRecordCount).Select(id => CreateIncomingTax(id, incomingPayment)));
        }

        private IEnumerable<IOutgoingTax> CreateOutgoingTaxes(IEnumerable<IOutgoingPayment> outgoingPayments)
        {
            return outgoingPayments.SelectMany(outgoingPayment => CreateIdentifiers(outgoingPayment.Id, TaxRecordCount).Select(id => CreateOutgoingTax(id, outgoingPayment)));
        }

        private IAccount CreateAccount(int id)
        {
            return new Account
            {
                Id = id,
                Number = CreateAccountNumber()
            };
        }

        private IIncomingPayment CreateIncomingPayment(int id, IAccount account)
        {
            return new IncomingPayment
            {
                Id = id,
                AccountId = account.Id,
                Amount = CreatePaymentAmount(),
                Date = CreatePaymentDate(),
                Type = CreateType<IncomingPaymentType>()
            };
        }

        private OutgoingPayment CreateOutgoingPayment(int id, IAccount account)
        {
            return new OutgoingPayment
            {
                Id = id,
                AccountId = account.Id,
                Amount = CreatePaymentAmount(),
                Date = CreatePaymentDate(),
                Type = CreateType<OutgoingPaymentType>()
            };
        }

        private IIncomingTax CreateIncomingTax(int id, IPayment incomingPayment)
        {
            return new IncomingTax
            {
                Id = id,
                PaymentId = incomingPayment.Id,
                Amount = CreateTaxAmount(incomingPayment.Amount),
                Date = CreatePaymentDate(),
                Type = CreateType<IncomingTaxType>()
            };
        }

        private IOutgoingTax CreateOutgoingTax(int id, IPayment outgoingPayment)
        {
            return new OutgoingTax
            {
                Id = id,
                PaymentId = outgoingPayment.Id,
                Amount = CreateTaxAmount(outgoingPayment.Amount),
                Date = CreatePaymentDate(),
                Type = CreateType<OutgoingTaxType>()
            };
        }

        private string CreateAccountNumber()
        {
            return String.Join(
                "-",
                Enumerable.Range(0, AccountNumberGroupCount)
                    .Select(group =>
                        String.Join(
                            String.Empty,
                            Enumerable.Range(0, AccountNumberInGroupCount).Select(number => random.Next(MaxAccountNumber)))));
        }

        private float CreatePaymentAmount()
        {
            return random.Next(MaxAmountValue);
        }

        private DateTime CreatePaymentDate()
        {
            return DateTime.Now.AddDays(-random.Next(DaysInYear));
        }

        private float CreateTaxAmount(float paymentAmount)
        {
            return random.Next(MaxTaxValue) * paymentAmount / 100;
        }

        private TEnumType CreateType<TEnumType>()
        {
            Array enumValues = Enum.GetValues(typeof(TEnumType));
            int enumValueCount = enumValues.Length;
            int index = random.Next(enumValueCount);
            return (TEnumType)enumValues.GetValue(index);
        }
    }
}