using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// A party of up to six pokemon, owned by the player or a trainer
public class PokemonParty : MonoBehaviour
{
    const int MaxPartySize = 6;

    [SerializeField] List<Pokemon> pokemons;

    public List<Pokemon> Pokemons => pokemons;

    private void Start()
    {
        foreach (var pokemon in pokemons)
            pokemon.Init();
    }

    // Returns the first pokemon that hasn't fainted, or null
    public Pokemon GetHealthyPokemon()
    {
        return pokemons.FirstOrDefault(x => x.HP > 0);
    }

    public void AddPokemon(Pokemon newPokemon)
    {
        // TODO: send to PC when the party is full
        if (pokemons.Count < MaxPartySize)
            pokemons.Add(newPokemon);
    }
}
