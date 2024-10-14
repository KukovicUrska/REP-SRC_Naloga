using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SRC_Naloga.DBConnection;

public partial class DbImdbContext : DbContext
{
    public DbImdbContext()
    {
    }

    public DbImdbContext(DbContextOptions<DbImdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<ActorMovie> ActorMovies { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=DB_IMDb; Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.IdActor).HasName("PK__Actor__28CBA09393213C2C");

            entity.ToTable("Actor");

            entity.Property(e => e.IdActor)
                .HasMaxLength(30)
                .HasColumnName("idActor");
            entity.Property(e => e.BornDate)
                .HasColumnType("date")
                .HasColumnName("bornDate");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(150)
                .HasColumnName("lastName");
        });

        modelBuilder.Entity<ActorMovie>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FkIdActor)
                .HasMaxLength(30)
                .HasColumnName("fk_idActor");
            entity.Property(e => e.FkIdMovie)
                .HasMaxLength(30)
                .HasColumnName("fk_idMovie");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.IdMovie).HasName("PK__Movie__1A9A9792B089A524");

            entity.ToTable("Movie");

            entity.Property(e => e.IdMovie)
                .HasMaxLength(30)
                .HasColumnName("idMovie");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Picture).HasColumnName("picture");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasColumnName("title");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
