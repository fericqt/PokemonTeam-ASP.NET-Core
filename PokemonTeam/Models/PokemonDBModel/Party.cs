using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokemonTeam.Models.PokemonDBModel;

[Table("Party")]
public partial class Party
{
    [Key]
    [Column("PartyID")]
    public int PartyId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Classification { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [InverseProperty("Party")]
    public virtual ICollection<PartyDetail> PartyDetails { get; set; } = new List<PartyDetail>();
}
