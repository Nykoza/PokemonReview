using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces;

public interface IReviewRepository
{
    Task<ICollection<Review>> GetReviews();
    Task<Review> GetReview(int reviewId);
    Task<ICollection<Review>> GetReviewsOfAPokemon(int pokeId);
    bool ReviewExists(int reviewId);
}