namespace Application.Consultants.Commands.UpdateMission;

using Application.Repositories;
using MediatR;

public class UpdateMissionCommandHandler(IMissionRepository repository) : IRequestHandler<UpdateMissionCommand>
{
    public async Task Handle(UpdateMissionCommand request, CancellationToken cancellationToken)
    {
        var mission = await repository.GetByIdAsync(id: request.MissionId);
        if (mission != null)
        {
            mission.EndDate = request.NewEndDate;
            await repository.SaveChangesAsync(cancellationToken);
        }
    }
}
