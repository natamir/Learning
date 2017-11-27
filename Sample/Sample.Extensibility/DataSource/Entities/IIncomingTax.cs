using Sample.Extensibility.DataSource.Enums;

namespace Sample.Extensibility.DataSource.Entities
{
    public interface IIncomingTax : ITax
    {
        IncomingTaxType Type { get; }
    }
}