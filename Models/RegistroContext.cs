using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Practicas_ASP.NET.Models;

public partial class RegistroContext : DbContext
{
    public RegistroContext()
    {
    }

    public RegistroContext(DbContextOptions<RegistroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthRegistro> AuthRegistros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("server=localhost; database=Registro; integrated security=true; TrustServerCertificate=true");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthRegistro>(entity =>
        {
            entity.HasKey(e => e.JwtId).HasName("PK__AuthRegi__32FE98A2A3552867");

            entity.ToTable("AuthRegistro");

            entity.Property(e => e.JwtId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AuditFechaGeneracion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Mail)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
