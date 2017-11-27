using System;

namespace Sample.Extensibility.DataSource.Entities
{
    public interface ITax : IDateAmount
    {
        int Id { get; }

        int PaymentId { get; }
    }
}