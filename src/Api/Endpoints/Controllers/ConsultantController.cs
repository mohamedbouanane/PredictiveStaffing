namespace Api.Endpoints.Controllers;

using Application.Consultants.Queries.GetConsultantsWithMissions;
using Application.Consultants.Commands.UpdateMission;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using MediatR;

[ApiController]
[Route("api/[controller]")]
public class ConsultantController(IMediator mediator) : ControllerBase
{
    /// <summary> Récupère tous les consultants avec leurs missions. </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetConsultantsWithMissionsQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }

    /// <summary> Met à jour la date de fin d'une mission. </summary>
    [HttpPut("missions/{missionId}")]
    public async Task<IActionResult> UpdateMission(int missionId, [FromBody] DateTime newEndDate)
    {
        var command = new UpdateMissionCommand
        {
            MissionId = missionId,
            NewEndDate = newEndDate
        };

        await mediator.Send(command);
        return NoContent();
    }
}
