using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Library.DiscordBot;

public class GameRoom
{
    public ulong Id { get; private set; }
    private List<DiscordMember> Members = new();
    public GameCommands Commands { get; } = new();

    public void AddMember(DiscordMember member)
    {
        Members.Add(member);
        Commands.AddPlayer(member.Username);
    }
    
    public async Task CreateRoomAsync(InteractionContext context)
    {
        var everyoneRole = context.Guild.EveryoneRole;

        // Crear las sobreescrituras de permisos
        var overwrites = new List<DiscordOverwriteBuilder>
        {
            // Denegar acceso a todos (everyone role)
            new DiscordOverwriteBuilder(everyoneRole)
                .Deny(Permissions.AccessChannels),
        };

        foreach (var member in Members)
        {
            overwrites.Add(
                // Permitir acceso al usuario
                new DiscordOverwriteBuilder(member)
                    .Allow(Permissions.AccessChannels)
            );
        }

        // Crear el canal privado con las sobreescrituras de permisos
        string battle = $"**{Members[0].Username.ToUpper()}** vs **{Members[1].Username.ToUpper()}**";
        var privateChannel = await context.Guild.CreateTextChannelAsync($"\ud83e\udd3c{battle.Replace(" ", "_")}", overwrites: overwrites);
        
        Id = privateChannel.Id;
        await privateChannel.SendMessageAsync(GameCommands.ShowCatalogue());
        string alert = $"Se ha encontrado partida {battle}: {privateChannel.Mention}\n";
        await context.Channel.SendMessageAsync(alert);
        await AlertPlayersStartingGame(alert);
    }

    public async Task DeleteRoom(InteractionContext context)
    {
        var channel = context.Guild.GetChannel(Id);
        await channel.DeleteAsync();
    }

    public async Task AlertPlayersStartingGame(string alert)
    {
        foreach (var member in Members)
        {
            var dmChannel = await member.CreateDmChannelAsync();
            await dmChannel.SendMessageAsync(alert);
        }
    }
}