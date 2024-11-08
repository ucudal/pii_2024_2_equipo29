namespace Library;
 
/// <summary>
/// Clase abstracta que contiene un diccionario de diccionarios con
/// las efectividades de los ataques para cada pokemon.
/// </summary>
 public abstract class DicTypeEffectivity
 {
     /// <summary>
     /// Diccionarios con las efectividades de los ataques para cada pokemon.
     /// <para>Se utiliza en <c>Calculate</c> como parte de la formula de ataque de los pokemons.</para>
     /// </summary>
     /// <remarks>
     /// Diccionario estático de estados donde cada <i><b>CLAVE</b></i> representa
     /// el nombre del <c>Move</c> de un pokemon y cada <i><b>VALOR</b></i> es otro diccionario
     /// donde cada <i><b>CLAVE</b></i> representa el nombre del <c>Type</c> del pokemon atacado
     /// y cada <i><b>VALOR</b></i> es la efectividad del ataque.
     /// </remarks>
     public static Dictionary<string, Dictionary<string, float>> Effectivity { get; } = new()
     {
        { "normal", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 1 },
                { "flying", 1 },
                { "poison", 1 },
                { "ground", 1 },
                { "rock", 0.5f },
                { "bug", 1 },
                { "ghost", 0 },
                { "steel", 0.5f },
                { "fire", 1 },
                { "water", 1 },
                { "grass", 1 },
                { "electric", 1 },
                { "psychic", 1 },
                { "ice", 1 },
                { "dragon", 1 },
                { "dark", 1 },
                { "fairy", 1 }
            }
        },
        { "fighting", new Dictionary<string, float>
            {
                { "normal", 2 },
                { "fighting", 1 },
                { "flying", 0.5f },
                { "poison", 0.5f },
                { "ground", 1 },
                { "rock", 2 },
                { "bug", 0.5f },
                { "ghost", 0 },
                { "steel", 2 },
                { "fire", 1 },
                { "water", 1 },
                { "grass", 1 },
                { "electric", 1 },
                { "psychic", 0.5f },
                { "ice", 2 },
                { "dragon", 1 },
                { "dark", 2 },
                { "fairy", 0.5f }
            }
        },
        { "flying", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 2 },
                { "flying", 1 },
                { "poison", 1 },
                { "ground", 1 },
                { "rock", 0.5f },
                { "bug", 2 },
                { "ghost", 1 },
                { "steel", 0.5f },
                { "fire", 1 },
                { "water", 1 },
                { "grass", 2 },
                { "electric", 0.5f },
                { "psychic", 1 },
                { "ice", 1 },
                { "dragon", 1 },
                { "dark", 1 },
                { "fairy", 1 }
            }
        },
        { "poison", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 1 },
                { "flying", 1 },
                { "poison", 0.5f },
                { "ground", 0.5f },
                { "rock", 0.5f },
                { "bug", 1 },
                { "ghost", 0.5f },
                { "steel", 0 },
                { "fire", 1 },
                { "water", 1 },
                { "grass", 2 },
                { "electric", 1 },
                { "psychic", 1 },
                { "ice", 1 },
                { "dragon", 1 },
                { "dark", 1 },
                { "fairy", 2 }
            }
        },
        { "ground", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 1 },
                { "flying", 0 },
                { "poison", 2 },
                { "ground", 1 },
                { "rock", 2 },
                { "bug", 0.5f },
                { "ghost", 1 },
                { "steel", 2 },
                { "fire", 2 },
                { "water", 1 },
                { "grass", 0.5f },
                { "electric", 2 },
                { "psychic", 1 },
                { "ice", 1 },
                { "dragon", 1 },
                { "dark", 1 },
                { "fairy", 1 }
            }
        },
        { "rock", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 0.5f },
                { "flying", 2 },
                { "poison", 1 },
                { "ground", 0.5f },
                { "rock", 1 },
                { "bug", 2 },
                { "ghost", 1 },
                { "steel", 0.5f },
                { "fire", 2 },
                { "water", 1 },
                { "grass", 1 },
                { "electric", 1 },
                { "psychic", 1 },
                { "ice", 2 },
                { "dragon", 1 },
                { "dark", 1 },
                { "fairy", 1 }
            }
        },
        { "bug", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 0.5f },
                { "flying", 0.5f },
                { "poison", 0.5f },
                { "ground", 1 },
                { "rock", 1 },
                { "bug", 1 },
                { "ghost", 0.5f },
                { "steel", 0.5f },
                { "fire", 0.5f },
                { "water", 1 },
                { "grass", 2 },
                { "electric", 1 },
                { "psychic", 2 },
                { "ice", 1 },
                { "dragon", 1 },
                { "dark", 2 },
                { "fairy", 0.5f }
            }
        },
        { "ghost", new Dictionary<string, float>
            {
                { "normal", 0 },
                { "fighting", 1 },
                { "flying", 1 },
                { "poison", 1 },
                { "ground", 1 },
                { "rock", 1 },
                { "bug", 1 },
                { "ghost", 2 },
                { "steel", 1 },
                { "fire", 1 },
                { "water", 1 },
                { "grass", 1 },
                { "electric", 1 },
                { "psychic", 2 },
                { "ice", 1 },
                { "dragon", 1 },
                { "dark", 0.5f },
                { "fairy", 1 }
            }
        },
        { "steel", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 1 },
                { "flying", 1 },
                { "poison", 1 },
                { "ground", 1 },
                { "rock", 2 },
                { "bug", 1 },
                { "ghost", 1 },
                { "steel", 0.5f },
                { "fire", 0.5f },
                { "water", 0.5f },
                { "grass", 1 },
                { "electric", 0.5f },
                { "psychic", 1 },
                { "ice", 2 },
                { "dragon", 1 },
                { "dark", 1 },
                { "fairy", 2 }
            }
        },
        { "fire", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 1 },
                { "flying", 1 },
                { "poison", 1 },
                { "ground", 1 },
                { "rock", 0.5f },
                { "bug", 2 },
                { "ghost", 1 },
                { "steel", 2 },
                { "fire", 0.5f },
                { "water", 0.5f },
                { "grass", 2 },
                { "electric", 1 },
                { "psychic", 1 },
                { "ice", 2 },
                { "dragon", 1 },
                { "dark", 1 },
                { "fairy", 1 }
            }
        },
        { "water", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 1 },
                { "flying", 1 },
                { "poison", 1 },
                { "ground", 2 },
                { "rock", 2 },
                { "bug", 1 },
                { "ghost", 1 },
                { "steel", 1 },
                { "fire", 2 },
                { "water", 0.5f },
                { "grass", 0.5f },
                { "electric", 1 },
                { "psychic", 1 },
                { "ice", 1 },
                { "dragon", 1 },
                { "dark", 1 },
                { "fairy", 1 }
            }
        },
        { "grass", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 1 },
                { "flying", 0.5f },
                { "poison", 0.5f },
                { "ground", 2 },
                { "rock", 2 },
                { "bug", 0.5f },
                { "ghost", 1 },
                { "steel", 0.5f },
                { "fire", 0.5f },
                { "water", 1 },
                { "grass", 1 },
                { "electric", 1 },
                { "psychic", 1 },
                { "ice", 1 },
                { "dragon", 1 },
                { "dark", 1 },
                { "fairy", 1 }
            }
        },
        { "electric", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 1 },
                { "flying", 1 },
                { "poison", 1 },
                { "ground", 0.5f },
                { "rock", 1 },
                { "bug", 1 },
                { "ghost", 1 },
                { "steel", 1 },
                { "fire", 1 },
                { "water", 2 },
                { "grass", 1 },
                { "electric", 1 },
                { "psychic", 1 },
                { "ice", 1 },
                { "dragon", 1 },
                { "dark", 1 },
                { "fairy", 1 }
            }
        },
        { "psychic", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 1 },
                { "flying", 1 },
                { "poison", 1 },
                { "ground", 1 },
                { "rock", 1 },
                { "bug", 1 },
                { "ghost", 1 },
                { "steel", 1 },
                { "fire", 1 },
                { "water", 1 },
                { "grass", 1 },
                { "electric", 1 },
                { "psychic", 1 },
                { "ice", 1 },
                { "dragon", 1 },
                { "dark", 1 },
                { "fairy", 1 }
            }
        },
        { "ice", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 1 },
                { "flying", 2 },
                { "poison", 1 },
                { "ground", 2 },
                { "rock", 1 },
                { "bug", 1 },
                { "ghost", 1 },
                { "steel", 0.5f },
                { "fire", 0.5f },
                { "water", 0.5f },
                { "grass", 2 },
                { "electric", 1 },
                { "psychic", 1 },
                { "ice", 0.5f },
                { "dragon", 2 },
                { "dark", 1 },
                { "fairy", 1 }
            }
        },
        { "dragon", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 1 },
                { "flying", 1 },
                { "poison", 1 },
                { "ground", 1 },
                { "rock", 1 },
                { "bug", 1 },
                { "ghost", 1 },
                { "steel", 0.5f },
                { "fire", 1 },
                { "water", 1 },
                { "grass", 1 },
                { "electric", 1 },
                { "psychic", 1 },
                { "ice", 2 },
                { "dragon", 2 },
                { "dark", 1 },
                { "fairy", 0 }
            }
        },
        { "dark", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 0.5f },
                { "flying", 1 },
                { "poison", 1 },
                { "ground", 1 },
                { "rock", 1 },
                { "bug", 1 },
                { "ghost", 2 },
                { "steel", 1 },
                { "fire", 1 },
                { "water", 1 },
                { "grass", 1 },
                { "electric", 1 },
                { "psychic", 2 },
                { "ice", 1 },
                { "dragon", 1 },
                { "dark", 0.5f },
                { "fairy", 0.5f }
            }
        },
        { "fairy", new Dictionary<string, float>
            {
                { "normal", 1 },
                { "fighting", 2 },
                { "flying", 1 },
                { "poison", 0.5f },
                { "ground", 1 },
                { "rock", 1 },
                { "bug", 1 },
                { "ghost", 1 },
                { "steel", 0.5f },
                { "fire", 0.5f },
                { "water", 1 },
                { "grass", 1 },
                { "electric", 1 },
                { "psychic", 1 },
                { "ice", 1 },
                { "dragon", 2 },
                { "dark", 2 },
                { "fairy", 1 }
            }
        }
    };
 }


 