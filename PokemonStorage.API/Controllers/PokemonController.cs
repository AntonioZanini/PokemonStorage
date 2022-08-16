using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonStorage.API.Data;
using PokemonStorage.API.ViewModel;

namespace PokemonStorage.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokemonController : ControllerBase
{
    private readonly Context _context;

    public PokemonController(Context context)
    {
        _context = context;
    }

    // GET: api/Pokemon
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PokemonViewModel>>> GetPokemons()
    {
        if (DatabaseIsNull()) { return DatabaseNullError(); }
        return await _context.Pokemons.Select(p => PokemonViewModel.FromModel(p)).ToListAsync();
    }

    // GET: api/Pokemon/5
    [HttpGet("{number}")]
    public async Task<ActionResult<PokemonViewModel>> GetPokemon(string number)
    {
        if (DatabaseIsNull()) { return DatabaseNullError(); }

        Model.Pokemon? pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Number == number);

        if (pokemon == null) { return NotFound(); }

        return PokemonViewModel.FromModel(pokemon);
    }

    // PUT: api/Pokemon/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPokemon(int id, PokemonViewModel pokemon)
    {
        if (id != pokemon.Id) { return BadRequest(); }

        _context.Entry(pokemon).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PokemonExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Pokemon
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<PokemonViewModel>> PostPokemon(PokemonViewModel pokemon)
    {
        if (DatabaseIsNull()) { return DatabaseNullError(); }

        _context.Pokemons.Add(pokemon.ToModel());
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPokemon), new { id = pokemon.Id }, pokemon);
    }

    // DELETE: api/Pokemon/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePokemon(int id)
    {
        if (DatabaseIsNull()) { return DatabaseNullError(); }

        Model.Pokemon? pokemon = await _context.Pokemons.FindAsync(id);
        if (pokemon == null)
        {
            return NotFound();
        }

        _context.Pokemons.Remove(pokemon);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private ObjectResult DatabaseNullError() => Problem("Entity set 'Context.Pokemons'  is null.");
    private bool DatabaseIsNull() => _context.Pokemons == null;
    private bool PokemonExists(int id)
    {
        return (_context.Pokemons?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
