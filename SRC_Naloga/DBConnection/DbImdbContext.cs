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

    public virtual DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=DB_IMDb; Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.IdActor).HasName("PK__Actor__28CBA093357CB4A6");

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

            entity.HasMany(d => d.TkIdMovies).WithMany(p => p.TkIdActors)
                .UsingEntity<Dictionary<string, object>>(
                    "ActorMovie",
                    r => r.HasOne<Movie>().WithMany()
                        .HasForeignKey("TkIdMovie")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ActorMovi__tk_id__38996AB5"),
                    l => l.HasOne<Actor>().WithMany()
                        .HasForeignKey("TkIdActor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ActorMovi__tk_id__37A5467C"),
                    j =>
                    {
                        j.HasKey("TkIdActor", "TkIdMovie").HasName("PK__ActorMov__8814954E38D52CDD");
                    });
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.IdMovie).HasName("PK__Movie__1A9A9792C75B41A1");

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
