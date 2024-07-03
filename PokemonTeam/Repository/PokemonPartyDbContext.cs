using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PokemonTeam.Models.PokemonDBModel;

namespace PokemonTeam.Repository;

public partial class PokemonPartyDbContext : DbContext
{
    public PokemonPartyDbContext()
    {
    }

    public PokemonPartyDbContext(DbContextOptions<PokemonPartyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Party> Parties { get; set; }

    public virtual DbSet<PartyDetail> PartyDetails { get; set; }

    public virtual DbSet<PokemonInfo> PokemonInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=FERIC;Initial Catalog=PokemonPartyDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PartyDetail>(entity =>
        {
            entity.HasOne(d => d.Party).WithMany(p => p.PartyDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartyDetails_Party");

            entity.HasOne(d => d.Pokemon).WithMany(p => p.PartyDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartyDetails_PokemonInfo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
