using Pokedex.Api.Features.Pokemon;
using Pokedex.Api.PokemonDB;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PokedexContext>(options =>
    options.UseSqlite("Data Source=pokemon.db"));

var webRootPath = Path.GetFullPath(
    Path.Combine(
        builder.Environment.ContentRootPath,
        "..",
        "wwwroot"
    )
);

builder.Services.AddSingleton(webRootPath);
builder.Services.AddHttpClient<PokemonAssetService>(httpClient =>
{
    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
});
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<PokemonMapper>();
builder.Services.AddHttpClient<PokemonApiService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors("Frontend");
app.UseStaticFiles();
app.AddPokemonEndpoints();

app.Run();


