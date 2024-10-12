# PII 2024 - Equipo 29

## Universidad Católica del Uruguay
**Facultad de Ingeniería y Tecnologías**  
**Asignatura:** Programación II  

## Integrantes del equipo
- **Leandro Pereira**: [LeandroPereira23](https://github.com/LeandroPereira23)
- **Matías Pérez**: [MatiXV23](https://github.com/MatiXV23)
- **Anna Paula Tomas**: [pauuuutomas](https://github.com/pauuuutomas)
- **Gabriel Fioritti**: [GabrielFioritti](https://github.com/GabrielFioritti)

## Desafíos de la entrega

### Formula de ataque de un Pokémon
Se estudió la manera en como implementar los ataques de los pokemons, para lo cual se encontró la siguiente fórmula:

![Formula de ataque](assets/formulaAtaque.png)

- **B**: Bonificación. Si el ataque es del mismo tipo que el Pokémon que lo lanza, toma un valor de 1.5. Si el ataque es de un tipo diferente, toma un valor de 1.
- **E**: Efectividad. Puede tomar los valores de 0, 0.5, 1 o 2. (Hay casos especiales donde puede tomar valores de 0.25 o 4. Esto puede pasar si el pokemon atacado tiene más de un tipo).
- **V**: Variación. Un valor aleatorio entre 85 y 100.
- **N**: Nivel del Pokémon atacante (Por defecto, 1).
- **A**: Ataque o ataque especial del Pokémon. Si el ataque es físico, se toma el ataque. Si es especial, se toma el ataque especial.
- **P**: Poder del ataque.
- **D**: Defensa del Pokémon rival. Si el ataque es físico, se toma la defensa física. Si es especial, se toma la defensa especial.

La formula fue extraida de [Wikidex](https://www.wikidex.net/wiki/Daño)

### Implementación de una pokédex para la búsqueda de pokemons
Se implementó una **Pokédex** para permitir que el usuario busque entre los 1025 pokémons, esto facilitará al usuario que vaya a utilizar el bot, a la hora de buscar los pokemons, podrá filtrar por cada tipo de pokemon o ordanar la lista por ID, nombre, vida, ataque o defensa. 
La Pokédex se encuentra disponible en el siguiente enlace:  
[Pokédex](https://pokemon-blog-api.netlify.app)

## Principios SOLID utilizados
- **SRP** (Principio de Responsabilidad Única)
- **DIP** (Prinicipio de Inversión de Dependencias)

## Patrones de diseño utilizados
- **Facade**: Se implementó una clase `Game` que centraliza todas las clases y la lógica necesaria para el funcionamiento del juego.

## Consultas realizadas
Se realizaron consultas al docente a cargo, sobre conceptos de **UML**, para obtener un mayor entendimiento y mejorar la implementación del diagrama.

## Pruebas realizadas para futuras entregas (no está el código en github)
- **Bot**: Se creó un bot básico en la plataforma **Discord** para realizar pruebas, con el objetivo de en un futuro implementar múltiples plataformas, como **Discord** o **Telegram**.
- **PokeAPI**: Para integrar la PokeAPI en el programa, investigamos cómo obtener todos los datos de los pokemons, incluyendo los datos necesarios para utilizar la formula de ataque previamente mencionada. Se han desarrollado pruebas exitosas para consumir la PokeAPI en C#. Enlace: [PokeAPI](https://pokeapi.co).
- **Patron observer**: Se tiene pensado utilizar el patron observer para que la clase player observe cuando un pokémon realice un ataque y así poder cambiar el turno del jugador.

## Notas
- EL diagrama **UML** y las tarjetas **CRC** están dentro de la carpeta **Diagramas** ubicadas dentro de **src**.