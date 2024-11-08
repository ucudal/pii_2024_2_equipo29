using Library;
using Library.DiscordBot;

namespace Program;

class Program
{
    static async Task Main(string[] args)
    {
        DiscordBot bot = new DiscordBot();
        await bot.Iniciate();
    }
}
