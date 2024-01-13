using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController: ControllerBase
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ReviewDto))]
    public async Task<IActionResult> GetReviews()
    {
        IEnumerable<ReviewDto> reviews = _mapper.Map<IEnumerable<ReviewDto>>(await _reviewRepository.GetReviews());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(reviews);
    }

    [HttpGet("{reviewId}")]
    [ProducesResponseType(200, Type = typeof(ReviewDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetReview(int reviewId)
    {
        if (!_reviewRepository.ReviewExists(reviewId))
            return NotFound();

        ReviewDto review = _mapper.Map<ReviewDto>(await _reviewRepository.GetReview((reviewId)));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(review);
    }
    
    [HttpGet("pokemon/{pokeId}")]
    [ProducesResponseType(200, Type = typeof(ReviewDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetReviewsOfAPokemon(int pokeId)
    {
        IEnumerable<ReviewDto> reviews = _mapper.Map<IEnumerable<ReviewDto>>(await _reviewRepository.GetReviewsOfAPokemon((pokeId)));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(reviews);
    }
}