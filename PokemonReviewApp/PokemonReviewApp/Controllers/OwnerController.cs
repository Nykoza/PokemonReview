using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OwnerController: ControllerBase
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    
    public OwnerController(IOwnerRepository ownerRepository,ICountryRepository countryRepository, IMapper mapper)
    {
        _ownerRepository = ownerRepository;
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
    public async Task<IActionResult> GetOwners()
    {
        IEnumerable<OwnerDto> owners = _mapper.Map<IEnumerable<OwnerDto>>(await _ownerRepository.GetOwners());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(owners);
    }

    [HttpGet("{ownerId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetOwner(int ownerId)
    {
        if (!_ownerRepository.OwnerExists(ownerId))
            return NotFound();

        OwnerDto owner = _mapper.Map<OwnerDto>(await _ownerRepository.GetOwner(ownerId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(owner);
    }

    [HttpGet("{ownerId}/pokemon")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPokemonByOwner(int ownerId)
    {
        if (!_ownerRepository.OwnerExists(ownerId))
            return NotFound();

        IEnumerable<PokemonDto> pokemons = _mapper.Map<IEnumerable<PokemonDto>>(await _ownerRepository.GetPokemonByOwner(ownerId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(pokemons);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateOwner([FromBody] OwnerDto ownerCreate, [FromQuery] int countryId)
    {
        if (ownerCreate == null)
            return BadRequest(ModelState);

        var owners = await _ownerRepository
            .GetOwners();
        var owner = owners.FirstOrDefault(c => c.Name.Trim().ToUpper() == ownerCreate.Name.Trim().ToUpper());

        if (owner != null)
        {
            ModelState.AddModelError("", "Owner already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ownerMap = _mapper.Map<Owner>(ownerCreate);
        ownerMap.Country = _countryRepository.GetCountry(countryId);

        if (!_ownerRepository.CreateOwner(ownerMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
    
    [HttpPut("{ownerId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerDto? updatedOwner)
    {
        if (updatedOwner == null)
            return BadRequest(ModelState);

        if (ownerId != updatedOwner.Id)
            return BadRequest(ModelState);

        if (!_ownerRepository.OwnerExists(ownerId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Owner ownerMap = _mapper.Map<Owner>(updatedOwner);

        if (!_ownerRepository.UpdateOwner(ownerMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
    
    [HttpDelete("{ownerId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteOwner(int ownerId)
    {
        if (!_ownerRepository.OwnerExists(ownerId))
        {
            return NotFound();
        }

        var ownerToDelete = await _ownerRepository.GetOwner(ownerId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_ownerRepository.DeleteOwner(ownerToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting owner");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}