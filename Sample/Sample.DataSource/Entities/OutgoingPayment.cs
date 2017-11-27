using System;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.DataSource.Enums;

namespace Sample.DataSource.Entities
{
    internal class OutgoingPayment : IOutgoingPayment
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public float Amount { get; set; }

        public OutgoingPaymentType Type { get; set; }

        public DateTime Date { get; set; }
    }
}