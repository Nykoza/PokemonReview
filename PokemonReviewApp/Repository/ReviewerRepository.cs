using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository;

public class ReviewerRepository: IReviewerRepository
{
    private readonly DataContext _context;

    public ReviewerRepository(DataContext context)
    {
        _context = context;
    }


    public async Task<ICollection<Reviewer>> GetReviewers()
    {
        return await _context.Reviewers.OrderBy(r => r.Id).ToListAsync();
    }

    public async Task<Reviewer> GetReviewer(int reviewerId)
    {
        return await _context.Reviewers.Where(r => r.Id == reviewerId).Include(r => r.Reviews).FirstOrDefaultAsync();
    }

    public async Task<ICollection<Review>> GetReviewsByReviewer(int reviewerId)
    {
        return await _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToListAsync();
    }

    public bool ReviewerExists(int reviewerId)
    {
        return _context.Reviewers.Any(r => r.Id == reviewerId);
    }

    public bool CreateReviewer(Reviewer reviewer)
    {
        _context.Add(reviewer);
        return Save();
    }

    public bool UpdateReviewer(Reviewer reviewer)
    {
        _context.Update(reviewer);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}