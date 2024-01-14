using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository;

public class OwnerRepository: IOwnerRepository
{
    private readonly DataContext _context;
    
    public OwnerRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Owner>> GetOwners()
    {
        return await _context.Owners.OrderBy(o => o.Id).ToListAsync();
    }

    public async Task<Owner> GetOwner(int ownerId)
    {
        return await _context.Owners.Where(o => o.Id == ownerId).FirstOrDefaultAsync();
    }

    public async Task<ICollection<Owner>> GetOwnerOfAPokemon(int pokemonId)
    {
        return await _context.PokemonOwners.Where(po => po.Pokemon.Id == pokemonId).Select(po => po.Owner)
            .ToListAsync();
    }

    public async Task<ICollection<Pokemon>> GetPokemonByOwner(int ownerId)
    {
        return await _context.PokemonOwners.Where(po => po.Owner.Id == ownerId).Select(po => po.Pokemon).ToListAsync();
    }

    public bool OwnerExists(int ownerId)
    {
        return _context.Owners.Any(o => o.Id == ownerId);
    }
    
    public bool CreateOwner(Owner owner)
    {
        _context.Add(owner);
        return Save();
    }

    public bool UpdateOwner(Owner owner)
    {
        _context.Update(owner);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0;
    }
}