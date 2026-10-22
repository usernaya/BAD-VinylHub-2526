using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Domain.Entities;
using VinylHub.Infrastructure.Identity;

namespace VinylHub.Infrastructure.Data
{
    public class VinylHubDbContext : IdentityDbContext<ApplicationUser>
    {
        public VinylHubDbContext(DbContextOptions<VinylHubDbContext> options) : base(options)
        {
            // leeg constructor
        }

        // dbsets
        public DbSet<Vinyl> Vinyls { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // relaties 
            modelBuilder.Entity<Vinyl>()
            .HasOne(v => v.Artist)
            .WithMany(a => a.Vinyls)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vinyl>()
                .HasOne(v => v.Genre)
                .WithMany(g => g.Vinyls)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Vinyl)
                .WithMany(v => v.Loans)
                .OnDelete(DeleteBehavior.Restrict);

            // seeding
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Pop" },
                new Genre { Id = 2, Name = "R&B" },
                new Genre { Id = 3, Name = "Rock" },
                new Genre { Id = 4, Name = "Electronic" },
                new Genre { Id = 5, Name = "Hip-Hop" }
            );

            modelBuilder.Entity<Artist>().HasData(
                new Artist { Id = 1, Name = "Ariana Grande" },
                new Artist { Id = 2, Name = "Billie Eilish" },
                new Artist { Id = 3, Name = "Sabrina Carpenter" },
                new Artist { Id = 4, Name = "Michael Jackson" },
                new Artist { Id = 5, Name = "The Weeknd" }
            );

        }
    }
}

