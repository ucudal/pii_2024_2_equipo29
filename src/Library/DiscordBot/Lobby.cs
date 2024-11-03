using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace Library.DiscordBot;

public class Lobby
{
    private List<DiscordMember> waitingPlayers = new List<DiscordMember>();
        
    public List<GameRoom> rooms = new List<GameRoom>();
    
    
    public async Task AddWaitingPlayer(CommandContext context)
    {
        DiscordMember member = context.Member;
        if (!IsWaitingPlayerByName(member.Username) && waitingPlayers.Count < 2)
        {
            waitingPlayers.Add(member);
            await context.Channel.SendMessageAsync($"{member.Username.ToUpper()} ha sido agregado a la lista de espera");
        }
        else
        {
            await context.Channel.SendMessageAsync($"{member.Username.ToUpper()} ya se encuentra esperando rival");
        }
    }
    private bool IsWaitingPlayerByName(string name)
    {
        foreach (var player in waitingPlayers)
        {
            if (player.Username == name) return true;
        }
        return false;
    }

    public async Task TryToStartGame(CommandContext context)
    {
        if (waitingPlayers.Count == 2)
        {
            GameRoom room = new GameRoom();
            foreach (DiscordMember member in waitingPlayers)
            {
                await room.AddMember(member, context);
            }
            await room.CreateRoomAsync(context);
            rooms.Add(room);

            waitingPlayers.Clear();
        }
    }
}