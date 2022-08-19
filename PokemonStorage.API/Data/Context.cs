using Microsoft.EntityFrameworkCore;
using PokemonStorage.API.Data.EntityConfiguration;
using PokemonStorage.API.Model;

namespace PokemonStorage.API.Data;

public class Context : DbContext
{
    public DbSet<Pokemon> Pokemons { get; set; } = null!;
    public DbSet<PokemonAbility> PokemonAbilities { get; set; } = null!;
    public DbSet<PokemonType> PokemonTypes { get; set; } = null!;

    public Context(DbContextOptions<Context> options) : base(options)
    {
        Database.EnsureCreated();
        //ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PokemonAbilityConfiguration());
        modelBuilder.ApplyConfiguration(new PokemonConfiguration());
    }

}
