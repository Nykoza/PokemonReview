using System.Collections;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces;

public interface IReviewerRepository
{
    Task<ICollection<Reviewer>> GetReviewers();
    Task<Reviewer> GetReviewer(int reviewerId);
    Task<ICollection<Review>> GetReviewsByReviewer(int reviewerId);
    bool ReviewerExists(int reviewerId);
}