using System.Threading.Tasks;

namespace CurrencyMonitor.NotifyService.Contracts
{
    public interface ITelegramNotifier
    {
        Task Start();

        Task Send(long chatId, string message);
    }
}