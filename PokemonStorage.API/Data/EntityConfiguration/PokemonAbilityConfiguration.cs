using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PokemonStorage.API.Data.EntityConfiguration;

public class PokemonAbilityConfiguration : IEntityTypeConfiguration<Model.Pokemon>
{
    public void Configure(EntityTypeBuilder<Model.Pokemon> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
