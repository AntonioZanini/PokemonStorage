using PokemonStorage.API.Model;

namespace PokemonStorage.API.Data;

public class DBInitializer : IDBInitializer
{
    Context _context;

    public DBInitializer(Context context)
    {
        _context = context;
    }

    public void Seed()
    {
        PokemonAbility overgrowth = new PokemonAbility()
        {
            Name = "Overgrow",
            Description = "Powers up Grass-type moves in a pinch."
        };

        Pokemon bulbasaur = new Pokemon()
        {
            Number = "1",
            Name = "Bulbasaur",
        };

        Pokemon ivysaur = new Pokemon()
        {
            Number = "2",
            Name = "Ivysaur",

        };
        ivysaur.Abilities.Add(overgrowth);
        bulbasaur.Abilities.Add(overgrowth);
        overgrowth.Pokemons.Add(ivysaur);
        overgrowth.Pokemons.Add(bulbasaur);

        _context.Add(bulbasaur);
        _context.Add(ivysaur);
        _context.Add(overgrowth);

        _context.SaveChanges();
    }
}
