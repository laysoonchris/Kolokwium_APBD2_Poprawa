using System.ComponentModel.DataAnnotations;

namespace KolokwiumAPBD2_Poprawa.Models;

public class Race
{
    [Key]
    public int RaceId { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Location { get; set; }
    public DateTime datetime { get; set; }
    
    public ICollection<TrackRace> TrackRaces { get; set; } = new HashSet<TrackRace>();
    
}