using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KolokwiumAPBD2_Poprawa.Models;

public class Track
{
    [Key]
    public int TrackId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [Precision(10,2)]
    public decimal LengthInKm { get; set; }
    
    public ICollection<TrackRace> TrackRaces { get; set; } = new HashSet<TrackRace>();
}