using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Library.DiscordBot
{
    public class DiscordCommands : BaseCommandModule
    {
        [Command("Test")]
        public async Task Test(CommandContext context)
        {
            await context.Channel.SendMessageAsync("Funciona");
        }
    }
}
