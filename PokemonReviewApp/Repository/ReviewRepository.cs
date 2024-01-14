using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository;

public class ReviewRepository : IReviewRepository
{
    private readonly DataContext _context;

    public ReviewRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Review>> GetReviews()
    {
        return await _context.Reviews.OrderBy(r => r.Id).ToListAsync();
    }

    public async Task<Review> GetReview(int reviewId)
    {
        return await _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefaultAsync();
    }

    public async Task<ICollection<Review>> GetReviewsOfAPokemon(int pokeId)
    {
        return await _context.Reviews.Where(r => r.Pokemon.Id == pokeId).OrderBy(r => r.Id).ToListAsync();
    }

    public bool ReviewExists(int reviewId)
    {
        return _context.Reviews.Any(r => r.Id == reviewId);
    }

    public bool CreateReview(Review review)
    {
        _context.Add(review);
        return Save();
    }
    
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}