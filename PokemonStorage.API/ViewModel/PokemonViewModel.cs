using PokemonStorage.API.Model;

namespace PokemonStorage.API.ViewModel;
public class PokemonViewModel : ModelBase
{
    public string Number { get; set; }
    public string Name { get; set; }
    public int[] TypesIds { get; set; }
    public string[] TypesNames { get; private set; }

    public PokemonViewModel()
    {
        Name = string.Empty;
        Number = string.Empty;
        TypesIds = new int[0];
        TypesNames = new string[0];
    }

    public static PokemonViewModel FromModel(Pokemon model)
    {
        return new PokemonViewModel()
        {
            Number = model.Number,
            Name = model.Name,
            TypesIds = model.Types.Select(t => t.Id).ToArray(),
            TypesNames = model.Types.Select(t => t.Name).ToArray()
        };
    }

    public Pokemon ToModel(IEnumerable<PokemonType>? types = null)
    {
        Pokemon model = new Pokemon()
        {
            Number = Number,
            Name = Name
        };

        if (types != null)
        {
            model.Types = types.Where(t => TypesIds.Contains(t.Id)).ToList();
        }

        return model;
    }


}
