using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RecordLabel.TheContext
{
    public class ReleaseContext : DbContext
    {
        public ReleaseContext() : base("EntityPlusTestDatabase")
        {

        }
        public ReleaseContext(string connectionString) : base(connectionString)
        {

        }

        public DbSet<Content> ContentEntries { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Metadata> Metadata { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Content>()
                .HasMany(x => x.Metadata)
                .WithMany(x => x.Targets)
                .Map(x => x.ToTable("Metadata", "JoinTables"));
           
            
            modelBuilder.Entity<Content>()
                .HasMany(x => x.References)
                .WithRequired(x => x.Owner);

            modelBuilder.Entity<Reference>()
                .ToTable("Content", "References");

            modelBuilder.Entity<Release>()
                .HasMany(x => x.Tracks)
                .WithRequired(x => x.Release);

            modelBuilder.Entity<Release>().ToTable("Releases");
            modelBuilder.Entity<Artist>().ToTable("Artists");
            modelBuilder.Entity<Metadata>().ToTable("Metadata");
        }        
    }
}
