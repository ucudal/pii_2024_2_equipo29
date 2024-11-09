using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Library.DiscordBot;

/// <summary>
/// Clase que representa una sala de juego en un bot de Discord.
/// </summary>
public class GameRoom
{
    /// <summary>
    /// Obtiene el ID del canal de la sala de batalla.
    /// </summary>
    /// <returns>
    ///     <c>ulong</c> con la ID de la sala
    /// </returns>
    public ulong Id { get; private set; }
    
    /// <summary>
    /// Obtiene una lista de <c>DiscordMember</c>
    /// </summary>
    /// <returns>
    ///     <c>List de DiscordMember</c>
    /// </returns>
    private List<DiscordMember> Members = new();
    
    /// <summary>
    /// Obtiene una instancia de <c>GameCommands</c>
    /// </summary>
    /// <returns>
    ///     Instancia de <c>GameCommands</c>
    /// </returns>
    public GameCommands Commands { get; } = new();

    /// <summary>
    /// Agrega un miembro a la sala de juego y lo registra en los comandos del juego.
    /// </summary>
    /// <param name="member">El miembro a agregar.</param>
    public void AddMember(DiscordMember member)
    {
        Members.Add(member);
        Commands.AddPlayer(member.Username);
    }
    
    /// <summary>
    /// Crea un canal de texto privado para la sala de juego en Discord.
    /// Configura permisos para que solo los jugadores puedan acceder a la sala.
    /// </summary>
    /// <param name="context">El contexto de la interacción de Discord.</param>
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

    /// <summary>
    /// Elimina una sala a través de su <c>Id</c> de forma asíncrona.
    /// </summary>
    /// <param name="context">
    /// El contexto del usuario y el lugar donde fue utilizado el comando.
    /// </param>
    public async Task DeleteRoom(InteractionContext context)
    {
        var channel = context.Guild.GetChannel(Id);
        await channel.DeleteAsync();
    }

    /// <summary>
    /// Envía una alerta al DM de los miembros (jugadores).
    /// Se utiliza para avisar a los jugadores emparejados que se encontró una partida.
    /// </summary>
    /// <param name="alert">
    /// Mensaje que se va a enviar al DM del jugador.
    /// </param>
    private async Task AlertPlayersStartingGame(string alert)
    {
        foreach (var member in Members)
        {
            var dmChannel = await member.CreateDmChannelAsync();
            await dmChannel.SendMessageAsync(alert);
        }
    }
}