using System;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.DataSource.Enums;

namespace Sample.DataSource.Entities
{
    internal class OutgoingTax : IOutgoingTax
    {
        public int Id { get; set; }

        public int PaymentId { get; set; }

        public float Amount { get; set; }

        public DateTime Date { get; set; }

        public OutgoingTaxType Type { get; set; }
    }
}