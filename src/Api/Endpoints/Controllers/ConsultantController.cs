namespace Api.Endpoints.Controllers;

using Application.Queries.GetConsultantsWithMissions;
using Application.Commands.UpdateMission;
using Microsoft.AspNetCore.Mvc;
using MediatR;

[ApiController]
[Route("api")]
public class ConsultantController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsultantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary> Récupère tous les consultants avec leurs missions. </summary>
    [HttpGet("consultants")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetConsultantsWithMissionsQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex) 
        {
            throw;
        }
    }

    /// <summary> Met à jour la date de fin d'une mission. </summary>
    [HttpPut("consultant/missions/{missionId}")]
    public async Task<IActionResult> UpdateMission(Guid missionId, [FromBody] DateTime newEndDate, CancellationToken cancellationToken)
    {
        var command = new UpdateMissionCommand
        {
            MissionId = missionId,
            NewEndDate = newEndDate
        };

        await _mediator.Send(command, cancellationToken);
        return Ok();
    }
}
