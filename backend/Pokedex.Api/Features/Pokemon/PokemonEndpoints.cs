using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;


namespace Pokedex.Api.Features.Pokemon;

public static class PokemonEndpoints
{
    public static IEndpointRouteBuilder AddPokemonEndpoints(this IEndpointRouteBuilder routebuilder)
    {
        var group = routebuilder.MapGroup("api/v1/pokemon");

        group.MapGet("/search/{pokemonName}",
            async (string pokemonName, IPokemonService pokemonService, CancellationToken cancellationToken) =>
            {
                var result = await pokemonService.GetPokemonListByNameAsync(pokemonName, cancellationToken);
                return Results.Ok(result);
            }
        );

        group.MapGet("/by-name/{pokemonName}",
            async (string pokemonName, IPokemonService pokemonService, CancellationToken cancellationToken) =>
            {
                var result = await pokemonService.GetPokemonByNameAsync(pokemonName, cancellationToken);
                return Results.Ok(result);
            }
        );
        group.MapPost("/import",
            async (IPokemonService pokemonService, CancellationToken cancellationToken) =>
            {
                await pokemonService.SaveAllPokemonInDatabase(cancellationToken);
                return Results.Ok("All Pokemon Saved");
            }
        );


        group.MapGet("/pokedex",
            async (IPokemonService pokemonService, CancellationToken cancellationToken) =>
            {
                var result = await pokemonService.GetAllPokemonList(cancellationToken);
                return Results.Ok(result);
            }
        );


        return routebuilder;
    }
}
