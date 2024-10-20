using DSharpPlus.CommandsNext;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
