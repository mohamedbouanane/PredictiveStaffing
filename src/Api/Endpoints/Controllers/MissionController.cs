namespace Api.Endpoints.Controllers;

using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using MediatR;

[ApiController]
[Route("api/[controller]")]
public class MissionController(IMediator mediator) : ControllerBase
{
    /// <summary> Récupère une mission par son ID. </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetMissionByIdQuery 
        { 
            MissionId = id 
        };

        var result = await mediator.Send(query);
        return Ok(result);
    }
}
