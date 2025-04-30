namespace Api.Mappers;

using Application.Models;
using AutoMapper;
using Api.Dtos;

public class MappingProfileMissions : Profile
{
    public MappingProfileMissions()
    {
        CreateMap<MissionModel, MissionDto>();
    }
}
