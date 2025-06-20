using KolokwiumAPBD2_Poprawa.Models;

namespace KolokwiumAPBD2_Poprawa.Data;
using Microsoft.EntityFrameworkCore;
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    public DbSet<Racer> Racers => Set<Racer>();
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<Race> Races => Set<Race>();
    public DbSet<TrackRace> TrackRaces => Set<TrackRace>();
    public DbSet<RaceParticipation> RaceParticipations => Set<RaceParticipation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Racer>().HasData(
            new Racer { RacerId = 1, FirstName = "Lewis", LastName = "Hamilton" },
            new Racer { RacerId = 2, FirstName = "Max", LastName = "Verstappen" }
        );

        modelBuilder.Entity<Track>().HasData(
            new Track { TrackId = 1, Name = "Silverstone Circuit", LengthInKm = 5.89m },
            new Track { TrackId = 2, Name = "Monaco Circuit", LengthInKm = 3.34m }
        );

        modelBuilder.Entity<Race>().HasData(
            new Race { RaceId = 1, Name = "British Grand Prix", Location = "Silverstone, UK", datetime = new DateTime(2025, 7, 14) },
            new Race { RaceId = 2, Name = "Monaco Grand Prix", Location = "Monte Carlo, Monaco", datetime = new DateTime(2025, 5, 25) }
        );

        modelBuilder.Entity<TrackRace>().HasData(
            new TrackRace { TrackRaceId = 1, TrackId = 1, RaceId = 1, Laps = 52 },
            new TrackRace { TrackRaceId = 2, TrackId = 2, RaceId = 2, Laps = 78 }
        );
    }
}