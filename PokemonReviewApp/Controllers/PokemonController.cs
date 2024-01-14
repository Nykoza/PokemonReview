using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokemonController: ControllerBase
{
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    
    public PokemonController(IPokemonRepository pokemonRepository, IOwnerRepository ownerRepository, IReviewRepository reviewRepository, IMapper mapper)
    {
        _pokemonRepository = pokemonRepository;
        _ownerRepository = ownerRepository;
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
    public IActionResult GetPokemons()
    {
        List<PokemonDto> pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(pokemons);
    }

    [HttpGet("{pokeId}")]
    [ProducesResponseType(200, Type = typeof(Pokemon))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemon(int pokeId)
    {
        if (!_pokemonRepository.PokemonExists(pokeId))
            return NotFound();
        
        PokemonDto pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId));
    
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
    
        return Ok(pokemon);
    }
    
    [HttpGet("{pokeId}/rating")]
    [ProducesResponseType(200, Type = typeof(decimal))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemonRating(int pokeId)
    {
        if (!_pokemonRepository.PokemonExists(pokeId))
            return NotFound();
    
        var rating = _pokemonRepository.GetPokemonRating(pokeId);
    
        if (!ModelState.IsValid)
            return BadRequest();
    
        return Ok(rating);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreatePokemon([FromBody] PokemonDto? pokemonCreate, [FromQuery] int ownerId, [FromQuery] int categoryId)
    {
        if (pokemonCreate == null)
            return BadRequest(ModelState);

        var pokemon = _pokemonRepository.GetPokemons()
            .FirstOrDefault(p => p.Name.Trim().ToUpper() == pokemonCreate.Name.Trim().ToUpper());

        if (pokemon != null)
        {
            ModelState.AddModelError("", "Pokemon already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);

        if (!_pokemonRepository.CreatePokemon(categoryId, ownerId, pokemonMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
    
    [HttpPut("{pokemonId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdatePokemon(int pokemonId, [FromQuery] int ownerId,
        [FromQuery] int categoryId, [FromBody] PokemonDto? updatedPokemon)
    {
        if (updatedPokemon == null)
            return BadRequest(ModelState);

        if (pokemonId != updatedPokemon.Id)
            return BadRequest(ModelState);

        if (!_pokemonRepository.PokemonExists(pokemonId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Pokemon pokemonMap = _mapper.Map<Pokemon>(updatedPokemon);

        if (!_pokemonRepository.UpdatePokemon(ownerId, categoryId, pokemonMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}