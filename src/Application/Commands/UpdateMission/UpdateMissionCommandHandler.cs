namespace Application.Commands.UpdateMission;

using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

public class UpdateMissionCommandHandler(IMissionRepository repository) : IRequestHandler<UpdateMissionCommand>
{
    public async Task Handle(UpdateMissionCommand request, CancellationToken cancellationToken)
    {
        var mission = await repository.GetByIdAsync
        (
            id: request.MissionId,
            cancellationToken: cancellationToken
        );

        if (mission != null)
        {
            var dateNow = DateTime.UtcNow;
            mission.EndDate = request.NewEndDate;
            mission.Status = request.NewEndDate < dateNow ? Domain.Enums.MissionStatus.Ended : Domain.Enums.MissionStatus.Active;

            await repository.SaveChangesAsync(cancellationToken);
        }
    }

    //public async Task<Unit> Handle(UpdateMissionCommand request, CancellationToken cancellationToken)
    //{
    //    var mission = await repository.GetByIdAsync(request.MissionId, cancellationToken);

    //    ArgumentNullException.ThrowIfNull(mission, nameof(mission));

    //    var dateNow = DateTime.UtcNow;
    //    mission.EndDate = request.NewEndDate;
    //    mission.Status = request.NewEndDate < dateNow
    //        ? MissionStatus.Ended
    //        : MissionStatus.Active;

    //    await repository.SaveChangesAsync(cancellationToken);

    //    return Unit.Value;
    //}
}
