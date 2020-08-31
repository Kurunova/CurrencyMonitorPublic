using System;
using System.Linq;
using System.Threading.Tasks;
using CurrencyMonitor.Core.Contracts;
using CurrencyMonitor.NotifyService.Configurations;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CurrencyMonitor.NotifyService.Services
{
    internal class TelegramNotifier : INotifier
    {
        private static TelegramBotClient Bot;

        private static ICurrencySubscribeService _currencySubscribeService;

        public TelegramNotifier(
            TelegramConfiguration telegramConfiguration, 
            ICurrencySubscribeService currencySubscribeService)
        {
            var token = telegramConfiguration.Token;
            Bot = new TelegramBotClient(token);

            _currencySubscribeService = currencySubscribeService;
        }

        public void Start()
        {
            Bot.OnMessage += BotOnMessageReceived;
            
            var me = Bot.GetMeAsync().Result;
            Bot.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"Start listening for @{me.Username}");
        }

        public void Stop()
        {
            Bot.StopReceiving();
        }

        public async Task Send(long chatId, string message)
        {
            await Bot.SendChatActionAsync(chatId, ChatAction.Typing);
            await Bot.SendTextMessageAsync(chatId, message);
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message == null || message.Type != MessageType.Text)
            {
                return;
            }

            switch (message.Text.Split(' ').First())
            {
                case "/start":
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                    await Task.Delay(500); // simulate longer running task
                    
                    _currencySubscribeService.Subscribe(message.Chat.Id);

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Вы подписаны");
                    break;
                case "/stop":
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                    await Task.Delay(500); // simulate longer running task

                    _currencySubscribeService.UnSubscribe(message.Chat.Id);

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Вы отписаны");
                    break;
                default:
                    const string usage = "Usage:\r\n/start   - Подписаться\r\n/stop - Отписаться";
                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        usage,
                        replyMarkup: new ReplyKeyboardRemove());
                    break;
            }
        }
    }
}