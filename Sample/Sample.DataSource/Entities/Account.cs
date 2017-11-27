using Sample.Extensibility.DataSource.Entities;

namespace Sample.DataSource.Entities
{
    internal class Account : IAccount
    {
        public int Id { get; set; }

        public string Number { get; set; }
    }
}