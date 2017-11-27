using System;

namespace Sample.Extensibility.DataSource.Entities
{
    public interface IPayment : IDateAmount
    {
        int Id { get; }

        int AccountId { get; }
    }
}