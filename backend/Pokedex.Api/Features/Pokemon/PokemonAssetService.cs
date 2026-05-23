using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Pokedex.Api.PokemonDB;
using SQLitePCL;

namespace Pokedex.Api.Features.Pokemon;

public class PokemonAssetService(
    string environment,
    PokedexContext dbContext,
    PokemonApiService apiService,
    HttpClient httpClient)
{
    public async Task DownloadPokemonIconAsync(
    int pokemonId,
    string iconFileName,
    CancellationToken cancellationToken)
    {
        var imageUrl =
            $"https://media.bisafans.de/a34ce16/pokemon/gen9/icon/statisch/{iconFileName}.png";

        var folderPath = Path.Combine(environment, "pokemon", "icons");
        Directory.CreateDirectory(folderPath);

        var filePath = Path.Combine(
            folderPath,
            $"{pokemonId:D3}.png"
        );

        Console.WriteLine($"Lade Icon für Id {pokemonId}: {imageUrl}");

        await using var imageStream =
            await httpClient.GetStreamAsync(imageUrl, cancellationToken);

        await using var fileStream = File.Create(filePath);

        await imageStream.CopyToAsync(fileStream, cancellationToken);
    }

    public async Task DownloadAllPokemonIconsAsync(CancellationToken cancellationToken)
    {
        var maxCount = await apiService.GetMaxPokemonCountAsync(cancellationToken);

        for (int pokemonCount = 1; pokemonCount <= maxCount; pokemonCount++)
        {
            var iconFileName = pokemonCount <= 905
            ? pokemonCount.ToString("D3")
            : ConvertGermanNameToBisafansFileName(
            await dbContext.Pokemons
            .AsNoTracking()
            .Where(p => p.Id == pokemonCount)
            .Select(p => p.GermanName)
            .FirstAsync(cancellationToken)
    );

            cancellationToken.ThrowIfCancellationRequested();
            await DownloadPokemonIconAsync(pokemonCount, iconFileName, cancellationToken);
        }
    }
    public async Task DownloadPokemonArtworkAsync(
    int pokemonId,
    string artworkFileName,
    CancellationToken cancellationToken)
    {
        var imageUrl =
            $"https://media.bisafans.de/abcd69b/pokemon/artwork/{artworkFileName}.png";

        Console.WriteLine(
            $"Lade Artwork für Id {pokemonId}: {imageUrl}"
        );

        var folderPath = Path.Combine(environment, "pokemon", "artworks");
        Directory.CreateDirectory(folderPath);

        var localFileName = pokemonId.ToString("D3");
        var filePath = Path.Combine(folderPath, $"{localFileName}.png");

        await using var imageStream =
            await httpClient.GetStreamAsync(imageUrl, cancellationToken);

        await using var fileStream = File.Create(filePath);

        await imageStream.CopyToAsync(fileStream, cancellationToken);

        Console.WriteLine(
            $"Artwork mit Id {pokemonId} in Path {filePath} gespeichert"
        );
    }
    public async Task DownloadAllPokemonArtworksAsync(
    CancellationToken cancellationToken)
    {
        var maxCount = await apiService.GetMaxPokemonCountAsync(cancellationToken);

        for (int pokemonCount = 1; pokemonCount <= maxCount; pokemonCount++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var artworkFileName = pokemonCount <= 905
                ? pokemonCount.ToString("D3")
                : ConvertGermanNameToBisafansFileName(
                    await dbContext.Pokemons
                        .AsNoTracking()
                        .Where(p => p.Id == pokemonCount)
                        .Select(p => p.GermanName)
                        .FirstAsync(cancellationToken)
                );

            await DownloadPokemonArtworkAsync(
                pokemonCount,
                artworkFileName,
                cancellationToken
            );
        }
    }

    private static string ConvertGermanNameToBisafansFileName(string germanName)
    {
        return germanName
            .ToLowerInvariant()
            .Replace("ä", "ae")
            .Replace("ö", "oe")
            .Replace("ü", "ue")
            .Replace("ß", "ss");
    }

}