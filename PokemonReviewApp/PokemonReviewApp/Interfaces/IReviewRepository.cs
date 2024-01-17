using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces;

public interface IReviewRepository
{
    Task<ICollection<Review>> GetReviews();
    Task<Review> GetReview(int reviewId);
    Task<ICollection<Review>> GetReviewsOfAPokemon(int pokeId);
    bool ReviewExists(int reviewId);
    bool CreateReview(Review review);
    bool UpdateReview(Review review);
    bool DeleteReview(Review review);
    bool DeleteReviews(List<Review> reviews);
    bool Save();
}