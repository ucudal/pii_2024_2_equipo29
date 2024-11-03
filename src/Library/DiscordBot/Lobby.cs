using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Library.DiscordBot;

public class Lobby
{
    private static Lobby instance;
    private List<DiscordMember> waitingPlayers = new();
    public List<GameRoom> rooms = new();

    private Lobby() { }
    
    public static Lobby GetInstance()
    {
        if (instance == null)
        {
            instance = new Lobby();
        }
        return instance;
    }
    
    public async Task AddWaitingPlayer(InteractionContext context)
    {
        DiscordMember member = context.Member;
        if (!IsWaitingPlayerByName(member.Username) && waitingPlayers.Count < 2)
        {
            waitingPlayers.Add(member);
            Console.WriteLine(waitingPlayers.Count);
            
            await context.Channel.SendMessageAsync($"{member.Username.ToUpper()} ha sido agregado a la lista de espera");
        }
        else
        {
            await context.Channel.SendMessageAsync($"{member.Username.ToUpper()} ya se encuentra esperando rival");
        }
    }
    
    private bool IsWaitingPlayerByName(string name)
    {
        return waitingPlayers.Any(player => player.Username == name);
    }

    public async Task TryToStartGame(InteractionContext context)
    {
        if (waitingPlayers.Count == 2)
        {
            GameRoom room = new GameRoom();
            foreach (DiscordMember member in waitingPlayers)
            { 
                room.AddMember(member);
            }
            await room.CreateRoomAsync(context);
            rooms.Add(room);

            waitingPlayers.Clear();
        }
    }
}