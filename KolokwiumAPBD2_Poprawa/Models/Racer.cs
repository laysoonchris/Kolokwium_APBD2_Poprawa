using System.ComponentModel.DataAnnotations;

namespace KolokwiumAPBD2_Poprawa.Models;

public class Racer
{
    [Key]
    public int RacerId { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    public ICollection<RaceParticipation> RaceParticipations { get; set; } = new List<RaceParticipation>();
}