namespace Api.Endpoints.Controllers;

using Application.Commands.UpdateMission;
using MediatR;
using Microsoft.AspNetCore.Mvc;

//[ApiController]
//[Route("api/[controller]")]
public class MissionController//(IMediator mediator) : ControllerBase
{
    ///// <summary> Récupère une mission par son ID. </summary>
    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    //{
    //    var query = new GetMissionByIdQuery 
    //    { 
    //        MissionId = id,
    //        cancellationToken = cancellationToken
    //    };

    //    var result = await mediator.Send(query);
    //    return Ok(result);
    //}
}
