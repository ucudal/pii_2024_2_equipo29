using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Library.DiscordBot;

public class Lobby
{
    private static Lobby instance;
    private List<DiscordMember> waitingPlayers = new();
    public List<GameRoom> rooms { get; } = new();

    private Lobby() { }
    
    public static Lobby GetInstance()
    {
        if (instance == null)
        {
            instance = new Lobby();
        }
        return instance;
    }
    
    public string AddWaitingPlayer(InteractionContext context)
    {
        DiscordMember member = context.Member;
        if (!IsWaitingPlayerByName(member.Username) && waitingPlayers.Count < Game.MaxPlayers)
        {
            waitingPlayers.Add(member);
            Console.WriteLine(waitingPlayers.Count);
            
            return $"{member.Username.ToUpper()} ha sido agregado a la lista de espera";
        }
        else
        {
            return $"{member.Username.ToUpper()} ya se encuentra esperando rival";
        }
    }
    
    private bool IsWaitingPlayerByName(string name)
    {
        return waitingPlayers.Any(player => player.Username == name);
    }

    public async Task TryToStartGame(InteractionContext context)
    {
        if (waitingPlayers.Count == Game.MaxPlayers)
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

    public async Task ClearLobby(InteractionContext context)
    {
        foreach (var room in rooms)
        {
            await room.DeleteRoom(context);
        }
        rooms.Clear();
    }

    public GameRoom GetGameRoomById(ulong id)
    {
        return rooms.Find(room => room.Id == id)!;
    }
}