using System.Collections;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces;

public interface IReviewerRepository
{
    Task<ICollection<Reviewer>> GetReviewers();
    Task<Reviewer> GetReviewer(int reviewerId);
    Task<ICollection<Review>> GetReviewsByReviewer(int reviewerId);
    bool ReviewerExists(int reviewerId);
    bool CreateReviewer(Reviewer reviewer);
    bool UpdateReviewer(Reviewer reviewer);
    bool DeleteReviewer(Reviewer reviewer);
    bool Save();
}