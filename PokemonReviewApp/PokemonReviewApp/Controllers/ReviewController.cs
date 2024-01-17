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
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IMapper _mapper;

    public ReviewController(IReviewRepository reviewRepository, IPokemonRepository pokemonRepository, IReviewerRepository reviewerRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _pokemonRepository = pokemonRepository;
        _reviewerRepository = reviewerRepository;
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
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async  Task<IActionResult> CreateReview([FromBody] ReviewDto? reviewCreate, [FromQuery] int pokemonId, [FromQuery] int reviewerId)
    {
        if (reviewCreate == null)
            return BadRequest(ModelState);

        var reviews = await _reviewRepository.GetReviews();
        var review = reviews.FirstOrDefault(r => r.Title.Trim().ToUpper() == reviewCreate.Title.Trim().ToUpper());

        if (review != null)
        {
            ModelState.AddModelError("", "Review already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Review reviewMap = _mapper.Map<Review>(reviewCreate);
        reviewMap.Pokemon = _pokemonRepository.GetPokemon(pokemonId);
        reviewMap.Reviewer = await _reviewerRepository.GetReviewer(reviewerId);
        
        if (!_reviewRepository.CreateReview(reviewMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
    
    [HttpPut("{reviewId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto? updatedReview)
    {
        if (updatedReview == null)
            return BadRequest(ModelState);

        if (reviewId != updatedReview.Id)
            return BadRequest(ModelState);

        if (!_reviewRepository.ReviewExists(reviewId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Review reviewMap = _mapper.Map<Review>(updatedReview);

        if (!_reviewRepository.UpdateReview(reviewMap))
        {
            ModelState.AddModelError("", "Something went wrong while updating the review");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
    
    [HttpDelete("{reviewId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteReview(int reviewId)
    {
        if (!_reviewRepository.ReviewExists(reviewId))
        {
            return NotFound();
        }

        var reviewToDelete = await _reviewRepository.GetReview(reviewId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_reviewRepository.DeleteReview(reviewToDelete))
        {
            ModelState.AddModelError("", "Something went wrong when deleting review");
        }

        return NoContent();
    }
}