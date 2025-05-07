namespace Application.Queries.GetConsultantsWithMissions;

using Application.Dtos;
using MediatR;

public sealed class GetConsultantsWithMissionsQuery : IRequest<List<ConsultantDto>>
{
}
