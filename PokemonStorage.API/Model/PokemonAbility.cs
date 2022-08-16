namespace PokemonStorage.API.Model;
public class PokemonAbility : ModelBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Pokemon> Pokemons { get; set; }
    public PokemonAbility()
    {
        Name = string.Empty;
        Description = string.Empty;
        Pokemons = new List<Pokemon>();
    }
}