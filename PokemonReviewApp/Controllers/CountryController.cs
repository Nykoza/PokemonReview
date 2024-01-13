using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    
    public CountryController(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
    [ProducesResponseType(400)]
    public IActionResult GetCountries()
    {
        IEnumerable<CountryDto> countries = _mapper.Map<IEnumerable<CountryDto>>(_countryRepository.GetCountries());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(countries);
    }

    [HttpGet("{countryId}")]
    [ProducesResponseType(200, Type = typeof(Country))]
    [ProducesResponseType(400)]
    public IActionResult GetCountry(int countryId)
    {
        if (!_countryRepository.CountryExists(countryId))
            return NotFound();

        CountryDto country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(country);
    }

    [HttpGet("owners/{ownerId}")]
    [ProducesResponseType(200, Type = typeof(Country))]
    [ProducesResponseType(400)]
    public IActionResult GetCountryByOwner(int ownerId)
    {
        CountryDto country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByOwner(ownerId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(country);
    }

    [HttpGet("{countryId}/owners")]
    public async Task<IActionResult> GetOwnersFromACountry(int countryId)
    {
        IEnumerable<OwnerDto> owners =
            _mapper.Map<List<OwnerDto>>(await _countryRepository.GetOwnersFromACountry(countryId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(owners);
    }
}