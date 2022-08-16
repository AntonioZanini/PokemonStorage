namespace PokemonStorage.API.Model;
public class PokemonType : ModelBase
{
    public string Name { get; set; }
    public ICollection<Pokemon> Pokemons { get; set; }

    public PokemonType()
    {
        Name = string.Empty;
        Pokemons = new List<Pokemon>();
    }
}
