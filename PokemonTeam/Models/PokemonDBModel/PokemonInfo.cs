using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokemonTeam.Models.PokemonDBModel;

[Table("PokemonInfo")]
public partial class PokemonInfo
{
    [Key]
    [Column("PokemonID")]
    public int PokemonId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Type { get; set; } = null!;

    [Column("AttackDMG", TypeName = "decimal(10, 3)")]
    public decimal AttackDmg { get; set; }

    [Column("HP", TypeName = "decimal(10, 3)")]
    public decimal Hp { get; set; }

    public int Lvl { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [InverseProperty("Pokemon")]
    public virtual ICollection<PartyDetail> PartyDetails { get; set; } = new List<PartyDetail>();
}
