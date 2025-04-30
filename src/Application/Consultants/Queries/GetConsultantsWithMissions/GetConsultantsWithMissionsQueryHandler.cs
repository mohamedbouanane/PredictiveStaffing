namespace Application.Consultants.Queries.GetConsultantsWithMissions;

using Application.Repositories;
using AutoMapper;
using MediatR;

public class GetConsultantsWithMissionsQueryHandler(IConsultantRepository repository, IMapper mapper) : IRequestHandler<GetConsultantsWithMissionsQuery, List<ConsultantDto>>
{
    public async Task<List<ConsultantDto>> Handle(GetConsultantsWithMissionsQuery request, CancellationToken cancellationToken)
    {
        var consultants = await repository.GetAllWithMissionsAsync(cancellationToken: cancellationToken);

        return mapper.Map<List<ConsultantDto>>(consultants);
    }
}
