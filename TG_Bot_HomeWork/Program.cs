using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TG_HomeworkBot
{

    class Program
    {
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            try
            {
                var end = true;
                var tgBot = new TG_Bot();
                while (end)
                {
                    var str = Console.ReadLine();
                    if (str == "exit") end = false;
                }
                Console.WriteLine("Good luck!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    internal class TG_Bot
    {
        private const string API_KEY = "8324380387:AAFdWIxndFerj2v7MPm-PZL68Wp9NTJIYVM";
        private TelegramBotClient _bot;
        
        public TG_Bot()
        {
            InitTelegramBot();
        }
        
        private async Task InitTelegramBot()
        {
            _bot = new TelegramBotClient(API_KEY);
            var me = await _bot.GetMe();
            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");

            _bot.OnMessage += OnMessage;
            _bot.OnUpdate += OnUpdate;
            _bot.OnError += OnError;
        }

        private async Task OnMessage(Message msg, UpdateType type)
        {
            if (msg.Text == "/start")
            {
                // await _bot.SendMessage(msg.Chat, "Welcome! Pick one direction",
                //     replyMarkup: new InlineKeyboardButton[] { "Left", "Right" });

                var replyMarkup = new ReplyKeyboardMarkup()
                {
                    Keyboard = new[]
                    {
                        new[] { new KeyboardButton("Создать группу") },
                        new[] { new KeyboardButton("Добавить ДЗ") },
                        new[] { new KeyboardButton("Просмотреть ДЗ") },
                        new[] { new KeyboardButton("Изменить ДЗ") },
                    },
                    ResizeKeyboard = true
                };
                
                var sent = 
                    await _bot.SendMessage(msg.Chat, "Who or Where are you?", replyMarkup: replyMarkup);
            }
        }
        private async Task OnUpdate(Update update)
        {
            if (update is { CallbackQuery: { } query }) // non-null CallbackQuery
            {
               // await _bot.AnswerCallbackQuery(query.Id, $"You picked {query.Data}");
               // await _bot.SendMessage(query.Message!.Chat, $"User {query.From} clicked on {query.Data}");
               await _bot.SendMessage(query.Message!.Chat, "Removing keyboard", replyMarkup: new ReplyKeyboardRemove());
            }
        }
        
        private async Task OnError(Exception exception, HandleErrorSource source)
        {
            Console.WriteLine(exception); // just dump the exception to the console
        }
    }
}