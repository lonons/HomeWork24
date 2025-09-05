using Telegram.Bot;
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
                    if (str != "exit") continue;
                    end = false; break;
                }
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
        }

        private async Task OnMessage(Message msg, UpdateType type)
        {
            if (msg.Text == "/start")
            {
                await _bot.SendMessage(msg.Chat, "Welcome! Pick one direction",
                    replyMarkup: new InlineKeyboardButton[] { "Left", "Right" });
            }
        }
        async Task OnUpdate(Update update)
        {
            if (update is { CallbackQuery: { } query }) // non-null CallbackQuery
            {
                await _bot.AnswerCallbackQuery(query.Id, $"You picked {query.Data}");
                await _bot.SendMessage(query.Message!.Chat, $"User {query.From} clicked on {query.Data}");
            }
        }
    }
}