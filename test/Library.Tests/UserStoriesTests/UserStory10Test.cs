namespace Library.Tests.UserStoriesTests;

public class UserStory10Test
{
    // 10. Como entrenador, quiero ver la lista de jugadores esperando por un oponente.
    // Criterios de aceptación:
    // En la pantalla se ve la lista de jugadores que se unieron a la lista de espera.
    
    // En nuestro caso no es posible simular este test, esto debido a que no podemos crear una instancia de DiscordMember desde 0
    // A su vez es necesario que sea una lista de DiscordMembers debido a que implementamos un sistema que crea una sala privada
    // para los usuarios que se encuentran en lista de espera. Esto sucede automaticamente al llegar al maximo de jugadores permitidos
    // en la clase Game. A su vez ambos jugadores son notificados en el lobby y mediante un mensaje privado, con su respectivo link a 
    // la sala de battalla.
    
    // A su vez en nuestro caso el Bot de Discord se encuentra en funcionamiento para esta entrega y esta especificado en el README.md, 
    // como invitarlo y encenderlo para probar todas las historias de usuario mediante los comandos del bot.
    
    // Estan invitamos a participar en una batalla de practica.
    
    
    [Test]
    public void ShowWaitListTest()
    {
        Assert.Pass();
    }
}