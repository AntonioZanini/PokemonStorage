using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonStorage.API.Model;

namespace PokemonStorage.API.Data.EntityConfiguration;

public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Model.Pokemon> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(p => p.Abilities).WithMany(a => a.Pokemons);
    }
}
