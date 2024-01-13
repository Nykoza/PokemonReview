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
    private readonly IMapper _mapper;
    
    public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
    {
        _ownerRepository = ownerRepository;
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
}