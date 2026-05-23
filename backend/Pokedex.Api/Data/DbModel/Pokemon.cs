using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Pokedex.Api.Data.DbModel;

public class PokemonEntity
{
    required public int Id { get; set; }
    required public string Name { get; set; }
    public string GermanName { get; set; } = string.Empty;
    required public string Type1 { get; set; }
    public string? Type2 { get; set; }
    //public string? imageFilePath { get; set; }
}
