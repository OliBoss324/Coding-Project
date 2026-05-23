using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Pokedex.Api.Features.Pokemon;

public class PokemonApi
{
    public int id { get; set; }
    public string? name { get; set; }
    public List<Types>? types { get; set; }
}

public class Types
{
    public int slot { get; set; }
    public Type? type { get; set; }
}

public class Type
{
    public string? name { get; set; }
}
