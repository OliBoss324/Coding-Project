using Pokedex.Api.Data.DbModel;
using Microsoft.EntityFrameworkCore;

namespace Pokedex.Api.PokemonDB;

public class PokedexContext : DbContext
{
    public PokedexContext(DbContextOptions<PokedexContext> options) : base(options)
    {
    }

    public DbSet<PokemonEntity> Pokemons { get; set; }
}
