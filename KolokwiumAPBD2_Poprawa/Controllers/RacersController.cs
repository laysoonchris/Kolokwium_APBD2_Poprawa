using KolokwiumAPBD2_Poprawa.Services;
using Microsoft.AspNetCore.Mvc;

namespace KolokwiumAPBD2_Poprawa.Controllers;

public class RacersController : ControllerBase
{
        private readonly IDbService _service;

    public RacersController(IDbService service)
    {
        _service = service;
    }

    [HttpGet("{id}/participations")]
    public async Task<IActionResult> GetParticipation(int id)
    {
        var result = await _service.GetRacerParticipationAsync(id);
        if (result == null) return NotFound("Racer not found");
        return Ok(result);
    }
}
