using System;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Pokedex.Api.PokemonDB;
using Pokedex.Api.Data.DbModel;
using Microsoft.EntityFrameworkCore;

namespace Pokedex.Api.Features.Pokemon;

public class PokemonMapper
{

    public PokemonEntity MapPokemonApiToPokemonEntity(PokemonApi pokemonapi)
    {
        var type1 = pokemonapi?.types?.FirstOrDefault(t => t.slot == 1);
        var type2 = pokemonapi?.types?.FirstOrDefault(t => t.slot == 2);
        var pokemon = new PokemonEntity()
        {
            Id = pokemonapi!.id,
            Name = pokemonapi.name!,
            Type1 = type1!.type!.name!,
            Type2 = type2?.type?.name,
        };
        return pokemon;
    }
}