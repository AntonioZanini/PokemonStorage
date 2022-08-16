namespace PokemonStorage.API.Model;
public class Pokemon : ModelBase
{
    public string Number { get; set; }
    public string Name { get; set; }
    public ICollection<PokemonType> Types { get; set; }
    public ICollection<PokemonAbility> Abilities { get; set; }

    public Pokemon()
    {
        Number = string.Empty;
        Name = string.Empty;
        Types = new List<PokemonType>();
        Abilities = new List<PokemonAbility>();
    }
}