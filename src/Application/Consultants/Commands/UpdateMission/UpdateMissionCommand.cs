namespace Application.Consultants.Commands.UpdateMission;

using MediatR;

public class UpdateMissionCommand : IRequest
{
    public Guid MissionId { get; set; }
    public DateTime NewEndDate { get; set; }
}
