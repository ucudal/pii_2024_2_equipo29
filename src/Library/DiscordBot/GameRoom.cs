using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace Library.DiscordBot;

public class GameRoom
{
    public ulong Id { get; private set; }
    public List<DiscordMember> Members { get;} = new List<DiscordMember>();

    public GameCommands commands  { get;} = new GameCommands();

    public async Task AddMember(DiscordMember member, CommandContext context)
    {
        Members.Add(member);
        commands.AddPlayer(member.Username);
    }
    
    public async Task CreateRoomAsync(CommandContext context)
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
        await context.Channel.SendMessageAsync($"Canal privado creado: {privateChannel.Mention}");
    }
}