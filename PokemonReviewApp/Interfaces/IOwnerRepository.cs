using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces;

public interface IOwnerRepository
{
    Task<ICollection<Owner>> GetOwners();
    Task<Owner> GetOwner(int ownerId);
    Task<ICollection<Owner>> GetOwnerOfAPokemon(int pokemonId);
    Task<ICollection<Pokemon>> GetPokemonByOwner(int ownerId);
    bool OwnerExists(int ownerId);
}