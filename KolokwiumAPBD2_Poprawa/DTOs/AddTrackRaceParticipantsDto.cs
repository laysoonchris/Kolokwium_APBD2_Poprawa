namespace KolokwiumAPBD2_Poprawa.DTOs;

public class AddTrackRaceParticipantsDto
{
    public string RaceName { get; set; } = null!;
    public string TrackName { get; set; } = null!;
    public List<ParticipationInputDto> Participations { get; set; } = new();
}

public class ParticipationInputDto
{
    public int RacerId { get; set; }
    public int Position { get; set; }
    public int FinishTimeInSeconds { get; set; }
}