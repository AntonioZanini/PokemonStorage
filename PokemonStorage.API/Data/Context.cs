using Microsoft.EntityFrameworkCore;
using PokemonStorage.API.Model;

namespace PokemonStorage.API.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) 
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<Pokemon> Pokemons { get; set; } = null!;
    public DbSet<PokemonAbility> PokemonAbilities { get; set; } = null!;
    public DbSet<PokemonType> PokemonTypes { get; set; } = null!;

}

