using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pokedex.Api.PokemonDB;
using Pokedex.Api.Data.DbModel;

namespace Pokedex.Api.Features.Pokemon
{
    public interface IPokemonService
    {
        Task SaveAllPokemonInDatabase(CancellationToken cancellationToken);

        Task<List<PokemonEntity>> GetPokemonListByNameAsync(string pokemonName, CancellationToken cancellationToken);

        Task<PokemonEntity?> GetPokemonByNameAsync(string pokemonName, CancellationToken cancellationToken);

        Task<List<PokemonEntity>> GetAllPokemonList(CancellationToken cancellationToken);
    }
}