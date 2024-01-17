﻿namespace PokemonReviewApp.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Owner> Owners { get; set; }
}