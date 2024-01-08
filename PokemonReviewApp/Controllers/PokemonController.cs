﻿using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class PokemonController: ControllerBase
{
    private readonly IPokemonRepository _pokemonRepository;
    
    public PokemonController(IPokemonRepository pokemonRepository)
    {
        Console.Write("Calling PokemonController constructor");
        _pokemonRepository = pokemonRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
    public IActionResult GetPokemons()
    {
        var pokemons = _pokemonRepository.GetPokemons();
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(pokemons);
    }
}