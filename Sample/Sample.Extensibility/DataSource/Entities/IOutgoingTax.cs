using Sample.Extensibility.DataSource.Enums;

namespace Sample.Extensibility.DataSource.Entities
{
    public interface IOutgoingTax : ITax
    {
        OutgoingTaxType Type { get; }
    }
}