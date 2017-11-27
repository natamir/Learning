using Sample.Extensibility.DataSource.Enums;

namespace Sample.Extensibility.DataSource.Entities
{
    public interface IIncomingPayment : IPayment
    {
        IncomingPaymentType Type { get; }
    }
}