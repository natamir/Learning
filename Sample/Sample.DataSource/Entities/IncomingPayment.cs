using System;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.DataSource.Enums;

namespace Sample.DataSource.Entities
{
    internal class IncomingPayment : IIncomingPayment
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public float Amount { get; set; }

        public IncomingPaymentType Type { get; set; }

        public DateTime Date { get; set; }
    }
}