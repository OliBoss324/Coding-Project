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

public class PokemonApiService(HttpClient httpClient)
{
    public async Task<PokemonApi?> GetPokemonApiCallByIdAsync(int id, CancellationToken cancellationToken)
    {
        var json = await httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}", cancellationToken);
        var pokemon = JsonSerializer.Deserialize<PokemonApi>(json);
        return pokemon;
    }


    public async Task<int> GetMaxPokemonCountAsync(CancellationToken cancellationToken)
    {
        var json = await httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon-species/", cancellationToken);
        using var apiresponse = JsonDocument.Parse(json);
        return apiresponse.RootElement.GetProperty("count").GetInt32();
    }

    public async Task<string> GetPokemonGermanNameByIdAsync(int id, CancellationToken cancellationToken)
    {
        var json = await httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon-species/{id}/", cancellationToken);
        using var apiResponse = JsonDocument.Parse(json);
        var germanName = apiResponse.RootElement
        .GetProperty("names")
        .EnumerateArray()
        .First(nameEntry =>
            nameEntry
                .GetProperty("language")
                .GetProperty("name")
                .GetString() == "de"
        )
        .GetProperty("name")
        .GetString();

        return germanName!.ToLowerInvariant();
    }
}