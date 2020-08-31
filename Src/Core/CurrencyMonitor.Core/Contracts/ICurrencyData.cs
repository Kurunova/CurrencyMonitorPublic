namespace CurrencyMonitor.Core.Contracts
{
    public interface ICurrencyData
    {
        string Type { get; set; }
        
        double Rate { get; set; }
    }
}