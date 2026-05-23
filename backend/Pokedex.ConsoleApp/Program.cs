// See https://aka.ms/new-console-template for more information
using Pokedex.Api.Features.Pokemon;
using Pokedex.Api.PokemonDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Runtime.InteropServices;
using Pokedex.Api.Data.DbModel;

var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
    "Mozilla/5.0"
);
var cancellationToken = new CancellationToken();
var testRootPath = Path.Combine(
    Directory.GetCurrentDirectory(),
    "..",
    "wwwroot"
);

var dbPath = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, "../../../../Pokedex.Api/pokemon.db")
);

var options = new DbContextOptionsBuilder<PokedexContext>()
    .UseSqlite($"Data Source={dbPath}")
    .Options;

var dbContext = new PokedexContext(options);
var apiService = new PokemonApiService(httpClient);
var mapper = new PokemonMapper();
var pokemonService = new PokemonService(dbContext, mapper, apiService);
var pokemonAssetService = new PokemonAssetService(testRootPath, dbContext, apiService, httpClient);

//var pokemon = await service.GetPokemonApiCallByIdAsync(1);
//var pokemonMaxCount = await service.GetMaxPokemonCountAsync();
//Console.WriteLine(pokemonMaxCount);
//await pokemonService.SaveAllPokemonInDatabase(cancellationToken);
//var pokename = Console.ReadLine();
//var pokemon = await pokemonService.GetPokemonByNameAsync(pokename);
//var pokemons = await pokemonService.GetPokemonListByNameAsync(pokename!, cancellationToken);
/* foreach (PokemonEntity pokemon in pokemons)
{
    Console.WriteLine($"Pokemon: {pokemon.Name} Pokemon Types {pokemon.Type1} {pokemon.Type2}");
} */
//Console.WriteLine(pokemons);

/* await pokemonAssetService.DownloadAllPokemonArtworksAsync(
    cancellationToken
); */