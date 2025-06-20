using KolokwiumAPBD2_Poprawa.DTOs;

namespace KolokwiumAPBD2_Poprawa.Services;

public interface IDbService
{
    Task<RacerParticipationResponseDto?> GetRacerParticipationAsync(int id);
    Task<string> AddRaceParticipantsAsync(AddTrackRaceParticipantsDto dto);
}