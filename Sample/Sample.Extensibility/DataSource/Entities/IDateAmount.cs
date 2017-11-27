using System;

namespace Sample.Extensibility.DataSource.Entities
{
    public interface IDateAmount
    {
        DateTime Date { get; }

        float Amount { get; }
    }
}