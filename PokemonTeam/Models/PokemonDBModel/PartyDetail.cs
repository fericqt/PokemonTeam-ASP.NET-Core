using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokemonTeam.Models.PokemonDBModel;

public partial class PartyDetail
{
    [Key]
    public int IdTrack { get; set; }

    [Column("PartyID")]
    public int PartyId { get; set; }

    [Column("PokemonID")]
    public int PokemonId { get; set; }

    [ForeignKey("PartyId")]
    [InverseProperty("PartyDetails")]
    public virtual Party Party { get; set; } = null!;

    [ForeignKey("PokemonId")]
    [InverseProperty("PartyDetails")]
    public virtual PokemonInfo Pokemon { get; set; } = null!;
}
