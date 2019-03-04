namespace DeltaX.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DeltaXModel : DbContext
    {
        public DeltaXModel()
            : base("name=DeltaXModel")
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Movy> Movies { get; set; }
        public virtual DbSet<MoviesActor> MoviesActors { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Actor>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Actor>()
                .Property(e => e.Bio)
                .IsFixedLength();

            modelBuilder.Entity<Movy>()
                .Property(e => e.Plot)
                .IsUnicode(false);

            modelBuilder.Entity<Producer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Producer>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
