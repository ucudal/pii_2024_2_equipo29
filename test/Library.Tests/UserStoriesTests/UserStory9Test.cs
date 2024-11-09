using DSharpPlus.Entities;
using Library.DiscordBot;
using Moq;

namespace Library.Tests.UserStoriesTests;

public class UserStory9Test
{
    // 9. Como entrenador, quiero unirme a la lista de jugadores esperando por un oponente.
    // Criterios de aceptación:
    // El jugador recibe un mensaje confirmando que fue agregado a la lista de espera.


    [Test]
    public async Task AddToWaitList()
    {
        var testMember = new Mock<DiscordMember>();     // Paquete Nugget Moq Agregado para simular una instancia de DiscordMember
        testMember.Setup(m => m.Username).Returns("Player1");
        Lobby lobby = Lobby.GetInstance();

        string msg = lobby.AddWaitingPlayer(testMember.Object);
        Assert.That(msg, Is.EqualTo($"**{testMember.Object.Username.ToUpper()}** se ha agregado a la lista de espera."));
    }
}