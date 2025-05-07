namespace Application.Mappers;

using Application.Dtos;
using Domain.Entities;
using AutoMapper;

public sealed class MissionMappingProfile : Profile
{
    public MissionMappingProfile()
    {
        CreateMap<Mission, MissionDto>();
    }
}
