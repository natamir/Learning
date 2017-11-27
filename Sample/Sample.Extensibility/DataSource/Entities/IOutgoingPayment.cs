using Sample.Extensibility.DataSource.Enums;

namespace Sample.Extensibility.DataSource.Entities
{
    public interface IOutgoingPayment : IPayment
    {
        OutgoingPaymentType Type { get; }
    }
}