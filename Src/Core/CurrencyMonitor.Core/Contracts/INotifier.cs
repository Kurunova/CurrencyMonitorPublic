using System.Threading.Tasks;

namespace CurrencyMonitor.Core.Contracts
{
    public interface INotifier
    {
        void Start();
        
        void Stop();

        Task Send(long chatId, string message);
    }
}