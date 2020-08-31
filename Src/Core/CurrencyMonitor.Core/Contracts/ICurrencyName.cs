namespace CurrencyMonitor.Core.Contracts
{
    public interface ICurrencyName
    {
        string Code { get; set; }

        string Name { get; set; }
    }
}