using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FRONT_web_personal_saving.Models;

public partial class BdPersonalSavingContext : DbContext
{
    public BdPersonalSavingContext()
    {
    }

    public BdPersonalSavingContext(DbContextOptions<BdPersonalSavingContext> options)
        : base(options)
    {
    }





    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.id_usuario).HasName("PK__TB_USUAR__4E3E04AD3CDD1533");

            entity.ToTable("TB_USUARIO");

            entity.HasIndex(e => e.email, "UQ__TB_USUAR__AB6E6164CFC27A81").IsUnique();

            entity.Property(e => e.id_usuario).HasColumnName("id_usuario");
            entity.Property(e => e.contraseña)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
           
            entity.Property(e => e.nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
