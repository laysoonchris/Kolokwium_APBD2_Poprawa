using KolokwiumAPBD2_Poprawa.DTOs;
using KolokwiumAPBD2_Poprawa.Services;
using Microsoft.AspNetCore.Mvc;

namespace KolokwiumAPBD2_Poprawa.Controllers;

public class TrackRacesController : ControllerBase
{
    private readonly IDbService _service;

    public TrackRacesController(IDbService service)
    {
        _service = service;
    }

    [HttpPost("participants")]
    public async Task<IActionResult> AddParticipants([FromBody] AddTrackRaceParticipantsDto dto)
    {
        var result = await _service.AddRaceParticipantsAsync(dto);
        
        if (result != "Participants added successfully")
            return BadRequest(result);
        return Ok(result);
    }
}