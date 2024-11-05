using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Library.DiscordBot;

public class GameRoom
{
    public ulong Id { get; private set; }
    public List<DiscordMember> Members { get; } = new();
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
        var privateChannel = await context.Guild.CreateTextChannelAsync("Battle", overwrites: overwrites);
        
        Id = privateChannel.Id;
        await privateChannel.SendMessageAsync($"Catálogo: {GameCommands.ShowCatalogue()}");
        await context.Channel.SendMessageAsync($"Canal privado creado: {privateChannel.Mention}");
    }

    public async Task DeleteRoom(InteractionContext context)
    {
        var guild = await context.Client.GetGuildAsync(Id);
        await guild.DeleteAsync();
    }
}