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
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace Pokedex.Api.Features.Pokemon;

public class PokemonService(
    PokedexContext dbContext,
    PokemonMapper mapper,
    PokemonApiService apiService
 ) : IPokemonService
{
    public async Task SaveAllPokemonInDatabase(CancellationToken cancellationToken)
    {
        var maxCount = await apiService.GetMaxPokemonCountAsync(cancellationToken);

        for (int pokemonCount = 1; pokemonCount <= maxCount; pokemonCount++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var pokemonResponse = await apiService.GetPokemonApiCallByIdAsync(
                pokemonCount,
                cancellationToken);

            var germanName = await apiService.GetPokemonGermanNameByIdAsync(
                pokemonCount,
                cancellationToken
            );

            if (pokemonResponse is null || string.IsNullOrWhiteSpace(germanName))
            {
                Console.WriteLine($"Pokemon mit Id {pokemonCount} nicht gefunden");
                continue;
            }

            Console.WriteLine(
                $"Pokemon mit Id {pokemonCount} und Namen {pokemonResponse.name} und {germanName}");

            var pokemonEntity = mapper.MapPokemonApiToPokemonEntity(pokemonResponse);

            pokemonEntity.GermanName = germanName;

            dbContext.Pokemons.Add(pokemonEntity);

            var existingPokemon = await dbContext.Pokemons
            .AnyAsync(pokemon => pokemon.Id == pokemonEntity.Id, cancellationToken);

            if (existingPokemon){dbContext.Pokemons.Update(pokemonEntity);}
            else{dbContext.Pokemons.Add(pokemonEntity);}
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<PokemonEntity?> GetPokemonByNameAsync(string pokemonName, CancellationToken cancellationToken)
    {
        var searchName = pokemonName.Trim().ToLower();
        var pokemon = await dbContext.Pokemons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Name == searchName, cancellationToken);
        if (pokemon == null)
        {
            Console.WriteLine("Pokemon nicht gefunden");
            return null;
        }
        return pokemon;
    }

    public async Task<List<PokemonEntity>> GetPokemonListByNameAsync(string pokemonName, CancellationToken cancellationToken)
    {
        var searchName = pokemonName.Trim().ToLower();
        List<PokemonEntity> pokemon = await dbContext.Pokemons
            .AsNoTracking()
            .Where(p => EF.Functions.Like(p.Name, $"{searchName}%"))
            .ToListAsync(cancellationToken);
        if (pokemon.Count == 0)
        {
            Console.WriteLine($"Pokemon mit Name {pokemonName} nicht gefunden");
            return pokemon;
        }
        return pokemon;
    }

    public async Task<List<PokemonEntity>> GetAllPokemonList(CancellationToken cancellationToken)
    {
        var result = await dbContext.Pokemons
            .AsNoTracking()
            .ToListAsync(cancellationToken);
            
        return result;
    }

}