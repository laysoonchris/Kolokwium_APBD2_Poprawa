using KolokwiumAPBD2_Poprawa.Data;
using KolokwiumAPBD2_Poprawa.DTOs;
using KolokwiumAPBD2_Poprawa.Models;
using Microsoft.EntityFrameworkCore;

namespace KolokwiumAPBD2_Poprawa.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<RacerParticipationResponseDto?> GetRacerParticipationAsync(int id)
    {
        var racer = await _context.Racers
            .Include(r => r.RaceParticipations)
            .ThenInclude(rp => rp.TrackRace)
            .ThenInclude(tr => tr.Race)
            .Include(r => r.RaceParticipations)
            .ThenInclude(rp => rp.TrackRace)
            .ThenInclude(tr => tr.Track)
            .FirstOrDefaultAsync(r => r.RacerId == id);
        
        if (racer == null) return null;

        return new RacerParticipationResponseDto
        {
            RacerId = racer.RacerId,
            FirstName = racer.FirstName,
            LastName = racer.LastName,
            Participations = racer.RaceParticipations.Select(rp => new ParticipationDto
            {
                FinishTimeInSeconds = rp.FinishTimeInSeconds,
                Position = rp.Position,
                Laps = rp.TrackRace.Laps,
                Race = new RaceDto
                {
                    Name = rp.TrackRace.Race.Name,
                    Location = rp.TrackRace.Race.Location,
                    Date = rp.TrackRace.Race.datetime
                },
                Track = new TrackDto
                {
                    Name = rp.TrackRace.Track.Name,
                    LengthInKm = rp.TrackRace.Track.LengthInKm
                }
            }).ToList()
        };
    }

    public async Task<string> AddRaceParticipantsAsync(AddTrackRaceParticipantsDto dto)
    {
        var race = await _context.Races.FirstOrDefaultAsync(r => r.Name == dto.RaceName);
        if (race == null) return "Race not found";
        
        var track = await _context.Tracks.FirstOrDefaultAsync(t => t.Name == dto.TrackName);
        if (track == null) return "Track not found";
        
        var trackRace = await _context.TrackRaces
            .Include(tr => tr.RaceParticipations)
            .FirstOrDefaultAsync(tr => tr.RaceId == race.RaceId && tr.TrackId == track.TrackId);
                if (trackRace == null) return "TrackRace entry not found";

        foreach (var participation in dto.Participations)
        {
            var racer = await _context.Racers.FindAsync(participation.RacerId);
            if (racer == null) return $"Racer with ID {participation.RacerId} not found";

            _context.RaceParticipations.Add(new RaceParticipation
            {
                RacerId = participation.RacerId,
                TrackRaceId = trackRace.TrackRaceId,
                Position = participation.Position,
                FinishTimeInSeconds = participation.FinishTimeInSeconds
            });

            if (trackRace.BestTimeInSeconds == null || participation.FinishTimeInSeconds < trackRace.BestTimeInSeconds)
            {
                trackRace.BestTimeInSeconds = participation.FinishTimeInSeconds;
            }
        }

        await _context.SaveChangesAsync();
        return "Participants added successfully";
    }
}