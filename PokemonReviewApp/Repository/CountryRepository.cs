﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository;

public class CountryRepository: ICountryRepository
{
    private readonly DataContext _context;

    public CountryRepository(DataContext context)
    {
        _context = context;
    }
    
    public bool CountryExists(int id)
    {
        return _context.Countries.Any(c => c.Id == id);
    }

    public ICollection<Country> GetCountries()
    {
        return _context.Countries.OrderBy(c => c.Id).ToList();
    }

    public Country GetCountry(int id)
    {
        return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
    }

    public Country GetCountryByOwner(int ownerId)
    {
        return _context.Owners.Where(o => o.Id == ownerId).Select(o => o.Country).FirstOrDefault();
    }

    public async Task<ICollection<Owner>> GetOwnersFromACountry(int countryId)
    {
        return await _context.Owners.Where(o => o.Country.Id == countryId).ToListAsync();
    }
}