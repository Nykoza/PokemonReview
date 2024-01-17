using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewerController: ControllerBase
{
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IMapper _mapper;

    public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
    {
        _reviewerRepository = reviewerRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ReviewerDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetReviewers()
    {
        ICollection<ReviewerDto> reviewers =
            _mapper.Map<ICollection<ReviewerDto>>(await _reviewerRepository.GetReviewers());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(reviewers);
    }

    [HttpGet("{reviewerId}")]
    [ProducesResponseType(200, Type = typeof(ReviewerDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetReviewer(int reviewerId)
    {
        if (!_reviewerRepository.ReviewerExists(reviewerId))
            return NotFound();

        ReviewerDto reviewer = _mapper.Map<ReviewerDto>(await _reviewerRepository.GetReviewer(reviewerId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(reviewer);
    }

    [HttpGet("{reviewerId}/reviews")]
    [ProducesResponseType(200, Type = typeof(ReviewDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetReviewsByReviewers(int reviewerId)
    {
        if (!_reviewerRepository.ReviewerExists(reviewerId))
            return NotFound();
        
        IEnumerable<ReviewDto> reviews = _mapper.Map<IEnumerable<ReviewDto>>(await _reviewerRepository.GetReviewsByReviewer(reviewerId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(reviews);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async  Task<IActionResult> CreateReviewer([FromBody] ReviewerDto? reviewerCreate)
    {
        if (reviewerCreate == null)
            return BadRequest(ModelState);

        var reviewers = await _reviewerRepository.GetReviewers();
        var reviewer = reviewers.FirstOrDefault(r => r.LastName.Trim().ToUpper() == reviewerCreate.LastName.Trim().ToUpper());

        if (reviewer != null)
        {
            ModelState.AddModelError("", "Reviewer already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Reviewer reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);
        
        if (!_reviewerRepository.CreateReviewer(reviewerMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
    
    [HttpPut("{reviewerId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerDto? updatedReviewer)
    {
        if (updatedReviewer == null)
            return BadRequest(ModelState);

        if (reviewerId != updatedReviewer.Id)
            return BadRequest(ModelState);

        if (!_reviewerRepository.ReviewerExists(reviewerId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Reviewer reviewerMap = _mapper.Map<Reviewer>(updatedReviewer);

        if (!_reviewerRepository.UpdateReviewer(reviewerMap))
        {
            ModelState.AddModelError("", "Something went wrong while updating the reviewer");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
    
    [HttpDelete("{reviewerId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteReviewer(int reviewerId)
    {
        if (!_reviewerRepository.ReviewerExists(reviewerId))
        {
            return NotFound();
        }

        var reviewerToDelete = await _reviewerRepository.GetReviewer(reviewerId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_reviewerRepository.DeleteReviewer(reviewerToDelete))
        {
            ModelState.AddModelError("", "Something went wrong when deleting reviewer");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}