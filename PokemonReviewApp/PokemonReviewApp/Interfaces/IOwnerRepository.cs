using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces;

public interface IOwnerRepository
{
    Task<ICollection<Owner>> GetOwners();
    Task<Owner> GetOwner(int ownerId);
    Task<ICollection<Owner>> GetOwnerOfAPokemon(int pokemonId);
    Task<ICollection<Pokemon>> GetPokemonByOwner(int ownerId);
    bool OwnerExists(int ownerId);
    bool CreateOwner(Owner owner);
    bool UpdateOwner(Owner owner);
    bool DeleteOwner(Owner owner);
    bool Save();
}